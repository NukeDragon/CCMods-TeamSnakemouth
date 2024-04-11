using HarmonyLib;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NukeDragon.TeamSnakemouth.Cards;

namespace NukeDragon.TeamSnakemouth
{
  internal class FreezeResistance : Artifact, IArtifact
  {
    int counter = 0;
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("FreezeResistance", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Leif_Deck.Deck,
          pools = [ArtifactPool.Common],
        },
        Sprite = ModEntry.Instance.FreezeResistanceSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "FreezeResistance", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "FreezeResistance", "description"]).Localize
      });
    }

    public override void OnTurnStart(State state, Combat combat)
    {
      counter++;
      if (counter < 3) return;
      combat.QueueImmediate(new AStatus()
      {
        status = ModEntry.Instance.Concentration_Status.Status,
        statusAmount = 1,
        targetPlayer = true,
        artifactPulse = this.Key()
      });
      counter = 0;
    }

    public override int? GetDisplayNumber(State s)
    {
      return counter;
    }

    public override List<Tooltip>? GetExtraTooltips() => StatusMeta.GetTooltips(ModEntry.Instance.Concentration_Status.Status, 1).ToList();
  }
}
