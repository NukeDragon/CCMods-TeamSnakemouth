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
  internal class TPSaver : Artifact, IArtifact
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("TPSaver", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Leif_Deck.Deck,
          pools = [ArtifactPool.EventOnly],
        },
        Sprite = ModEntry.Instance.TPSaverSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "TPSaver", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "TPSaver", "description"]).Localize
      });
    }
  }
}
