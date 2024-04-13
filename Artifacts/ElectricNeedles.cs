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
  internal class ElectricNeedles : Artifact, IArtifact
  {
    int counter = 0;
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("ElectricNeedles", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Vi_Deck.Deck,
          pools = [ArtifactPool.EventOnly],
        },
        Sprite = ModEntry.Instance.ElectricNeedlesSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ElectricNeedles", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ElectricNeedles", "description"]).Localize
      });
    }

    public void ModifyAAttack(ref AAttack attack, State s, Combat c)
    {
      if (attack.whoDidThis == ModEntry.Instance.Vi_Deck.Deck)
      {
        counter++;
        if (counter >= 5)
        {
          attack.stunEnemy = true;
          this.Pulse();
          counter = 0;
        }
      }
    }

    public override int? GetDisplayNumber(State s)
    {
      return counter;
    }
  }
}
