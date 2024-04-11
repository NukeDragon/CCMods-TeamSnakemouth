using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib;

namespace NukeDragon.TeamSnakemouth;

internal sealed class QueensPermit : Artifact, IArtifact
{
  public int countermax = 45;
  public int countercurrent;
  public bool thisexists = false;
  public bool incombat = false;
  public static void Register(IModHelper helper)
  {
    helper.Content.Artifacts.RegisterArtifact("QueensPermit", new()
    {
      ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
      Meta = new()
      {
        owner = Deck.colorless,
        pools = [ArtifactPool.Boss],
        unremovable = true
      },
      Sprite = ModEntry.Instance.QueensPermitSprite.Sprite,
      Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "QueensPermit", "name"]).Localize,
      Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "QueensPermit", "descriptionlogbook"]).Localize
    });
    ModEntry.Instance.Harmony.Patch(AccessTools.DeclaredMethod(typeof(Artifact), nameof(GetTooltips)), postfix: new HarmonyMethod(typeof(QueensPermit), nameof(Artifact_GetTooltips_Postfix)));
  }

  public override Spr GetSprite()
  {
    if (thisexists)
    {
      if (!incombat) return ModEntry.Instance.QueensPermitTPSprite.Sprite;
    }
    return ModEntry.Instance.QueensPermitSprite.Sprite;
  }
  private static void Artifact_GetTooltips_Postfix(Artifact __instance, ref List<Tooltip> __result)
  {
    if (__instance is not QueensPermit)
      return;
    var artifact1 = __instance as QueensPermit;
    var textTooltip = __result.OfType<TTText>().FirstOrDefault(t => t.text.StartsWith("<c=artifact>"));
    if (textTooltip is null)
      return;

    if (!artifact1!.thisexists)
      return;
    if (artifact1!.incombat)
    {
      textTooltip.text = DB.Join(
      "<c=artifact>{0}</c>\n".FF(__instance.GetLocName()),
        ModEntry.Instance.Localizations.Localize(["artifact", "QueensPermit", "descriptioncombat"], new
        {
          max = artifact1!.countermax,
        })      
      );
      return;
    }
    textTooltip.text = DB.Join(
      "<c=artifact>{0}</c>\n".FF(__instance.GetLocName()),
        ModEntry.Instance.Localizations.Localize(["artifact", "QueensPermit", "description"], new
        {
          current = artifact1!.countercurrent,
          max = artifact1!.countermax,
        })
      );
  }

  public override void OnCombatEnd(State state)
  {
    countercurrent = state.ship.Get(ModEntry.Instance.TP_Status.Status);
    incombat = false;
  }
  public override void OnReceiveArtifact(State s)
  {
    thisexists = true;
    foreach (var artifact in s.artifacts)
    {
      if (artifact is ExplorersPermit explorersPermit)
      {
        countermax = explorersPermit.countermax + 15;
        explorersPermit.OnRemoveArtifact(s);
      }
    }
    s.artifacts.RemoveAll(r => r is ExplorersPermit);
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
        AStatus a = new AStatus();
        a.status = ModEntry.Instance.TP_Status.Status;
        a.statusAmount = countermax;
        a.mode = AStatusMode.Set;
        a.targetPlayer = true;
        a.artifactPulse = this.Key();
        c.QueueImmediate((CardAction) a);
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
