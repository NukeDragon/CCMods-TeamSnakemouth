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
  internal class TPSaver : Artifact, IArtifact, ISnakemouthHook
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("TPSaver", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Leif_Deck.Deck,
          pools = [ArtifactPool.Common],
        },
        Sprite = ModEntry.Instance.TPSaverSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "TPSaver", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "TPSaver", "description"]).Localize
      });
    }

    public void OnATPCost(ATPCostAction action, State s, Combat c)
    {
      if (action.cost >= 5) c.QueueImmediate(new AStatus
      {
        status = ModEntry.Instance.TP_Status.Status,
        statusAmount = 1,
        artifactPulse = this.Key(),
        targetPlayer = true,
        canRunAfterKill = true
      });
    }
  }
}
