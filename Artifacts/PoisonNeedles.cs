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
  internal class PoisonNeedles : Artifact, IArtifact
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("PoisonNeedles", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Vi_Deck.Deck,
          pools = [ArtifactPool.EventOnly],
        },
        Sprite = ModEntry.Instance.PoisonNeedlesSprite.Sprite, 
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "PoisonNeedles", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "PoisonNeedles", "description"]).Localize
      });
    }
  }
}
