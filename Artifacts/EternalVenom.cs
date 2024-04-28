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
  internal class EternalVenom : Artifact, IArtifact
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("EternalVenom", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Vi_Deck.Deck,
          pools = [ArtifactPool.Boss],
        },
        Sprite = ModEntry.Instance.EternalVenomSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EternalVenom", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EternalVenom", "description"]).Localize
      });
    }

    public override List<Tooltip>? GetExtraTooltips() => StatusMeta.GetTooltips(ModEntry.Instance.Poison_Status.Status, 1);
  }
}
