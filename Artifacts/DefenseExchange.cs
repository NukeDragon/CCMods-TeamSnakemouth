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
  internal class DefenseExchange : Artifact, IArtifact
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("DefenseExchange", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Kabbu_Deck.Deck,
          pools = [ArtifactPool.Boss],
        },
        Sprite = ModEntry.Instance.DefenseExchangeSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "DefenseExchange", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "DefenseExchange", "description"]).Localize
      });
    }
  }
}
