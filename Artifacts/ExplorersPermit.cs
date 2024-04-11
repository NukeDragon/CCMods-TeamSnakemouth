using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace NukeDragon.TeamSnakemouth;

internal sealed class ExplorersPermit : Artifact, IArtifact
{
  public int countermax = 30;
  public int countercurrent = 1;
  public bool thisexists = false;
  public bool incombat = false;

  public static void Register(IModHelper helper)
  {
    helper.Content.Artifacts.RegisterArtifact("ExplorersPermit", new()
    {
      ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
      Meta = new()
      {
        owner = Deck.colorless,
        pools = [ArtifactPool.EventOnly],
        unremovable = true
      },
      Sprite = ModEntry.Instance.ExplorersPermitSprite.Sprite,
      Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ExplorersPermit", "name"]).Localize,
      Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ExplorersPermit", "descriptionlogbook"]).Localize
    });
    ModEntry.Instance.Harmony.Patch(AccessTools.DeclaredMethod(typeof(Artifact), nameof(GetTooltips)), postfix: new HarmonyMethod(typeof(ExplorersPermit), nameof(Artifact_GetTooltips_Postfix)));
  }

  public override Spr GetSprite()
  {
    if (thisexists & !incombat) return ModEntry.Instance.ExplorersPermitTPSprite.Sprite;
    return ModEntry.Instance.ExplorersPermitSprite.Sprite;
  }

  private static void Artifact_GetTooltips_Postfix(Artifact __instance, ref List<Tooltip> __result)
  {
    if (__instance is not ExplorersPermit)
      return;
    var artifact1 = __instance as ExplorersPermit;
    var textTooltip = __result.OfType<TTText>().FirstOrDefault(t => t.text.StartsWith("<c=artifact>"));
    if (textTooltip is null)
      return;

    if (!artifact1!.thisexists)
      return;
    if (artifact1!.incombat)
    {
      textTooltip.text = DB.Join(
      "<c=artifact>{0}</c>\n".FF(__instance.GetLocName()),
        ModEntry.Instance.Localizations.Localize(["artifact", "ExplorersPermit", "descriptioncombat"], new
        {
          max = (int)(artifact1!.countermax),
        })
      );
      return;
    }
    textTooltip.text = DB.Join(
      "<c=artifact>{0}</c>\n".FF(__instance.GetLocName()),
        ModEntry.Instance.Localizations.Localize(["artifact", "ExplorersPermit", "description"], new
        {
          current = (int)(artifact1!.countercurrent),
          max = (int)(artifact1!.countermax),
        })
      );
  }

  public override void OnCombatEnd(State state)
  {
    countercurrent = state.ship.Get(ModEntry.Instance.TP_Status.Status);
    incombat = false;
  }

  public override void OnReceiveArtifact(State state)
  {
    thisexists = true;
    countercurrent = countermax;
  }

  public override void OnTurnStart(State s, Combat c)
  {
    if (c.turn != 1)
      return;
    AStatus a = new()
    {
      status = ModEntry.Instance.TP_Status.Status,
      statusAmount = countercurrent,
      mode = AStatusMode.Set,
      targetPlayer = true,
      artifactPulse = this.Key()
    };
    c.QueueImmediate((CardAction)a);
    countercurrent = 0;
    incombat = true;
  }

  public override void AfterPlayerStatusAction(State s, Combat c, Status status, AStatusMode mode, int statusAmount)
  {
    if (status == ModEntry.Instance.TP_Status.Status)
    {
      if (s.ship.Get(ModEntry.Instance.TP_Status.Status) > countermax)
      {
        AStatus a = new()
        {
          status = ModEntry.Instance.TP_Status.Status,
          statusAmount = countermax,
          mode = AStatusMode.Set,
          targetPlayer = true,
          artifactPulse = this.Key()
        };
        c.QueueImmediate((CardAction)a);
      }
    }
  }

  public override int? GetDisplayNumber(State s)
  {
    if (incombat) return countermax;
    return countercurrent;
  }

  public override List<Tooltip>? GetExtraTooltips()
  {
    return StatusMeta.GetTooltips(ModEntry.Instance.TP_Status.Status, countercurrent).ToList();
  }
}
