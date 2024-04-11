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
  internal class Template : Artifact, IArtifact
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          //owner = ModEntry.Instance..Deck,
          pools = [ArtifactPool.Boss],
        },
        Sprite = ModEntry.Instance.ExplorersPermitSprite.Sprite, //make sure to change this!
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "", "description"]).Localize
      });
    }
  }
}
