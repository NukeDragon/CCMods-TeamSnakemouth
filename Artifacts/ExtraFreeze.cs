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
  internal class ExtraFreeze : Artifact, IArtifact
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("ExtraFreeze", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Leif_Deck.Deck,
          pools = [ArtifactPool.Boss],
        },
        Sprite = ModEntry.Instance.ExtraFreezeSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ExtraFreeze", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ExtraFreeze", "description"]).Localize
      });
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
      return FrozenManager.GetTooltips();
    }
  }
}
