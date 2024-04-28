using HarmonyLib;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NukeDragon.TeamSnakemouth.Cards;
using System.Diagnostics.Metrics;

namespace NukeDragon.TeamSnakemouth
{
  internal class PoisonNeedles : Artifact, IArtifact
  {
    int counter = 0;
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("PoisonNeedles", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Vi_Deck.Deck,
          pools = [ArtifactPool.Common],
        },
        Sprite = ModEntry.Instance.PoisonNeedlesSprite.Sprite, 
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "PoisonNeedles", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "PoisonNeedles", "description"], new
        {
          vicolor = ModEntry.Instance.Vi_Deck.Configuration.Definition.color.ToString(),
        }).Localize
      });
    }

    public void ModifyAAttack(ref AAttack attack, State s, Combat c)
    {
      if (attack.whoDidThis == ModEntry.Instance.Vi_Deck.Deck)
      {
        counter++;
        if (counter >= 4)
        {
          attack.SetStatus2(ModEntry.Instance.Poison_Status.Status);
          attack.SetStatus2Amount(1);
          this.Pulse();
          counter = 0;
        }
      }
    }

    public override int? GetDisplayNumber(State s)
    {
      return counter;
    }

    public override List<Tooltip>? GetExtraTooltips() => StatusMeta.GetTooltips(ModEntry.Instance.Poison_Status.Status, 1);
  }
}
