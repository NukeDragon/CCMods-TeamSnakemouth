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
  internal class TardigradeShield : Artifact, IArtifact
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("TardigradeShield", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Kabbu_Deck.Deck,
          pools = [ArtifactPool.Common],
        },
        Sprite = ModEntry.Instance.TardigradeShieldSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "TardigradeShield", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "TardigradeShield", "description"]).Localize
      });
    }
    public override void OnCombatStart(State state, Combat combat)
    {
      combat.QueueImmediate(new AAddCard()
      {
        card = new Sturdy(),
        destination = CardDestination.Hand,
        amount = 1
      });
    }
    public override List<Tooltip>? GetExtraTooltips()
    {
      List<Tooltip> extraTooltips = new List<Tooltip>();
      extraTooltips.Add(new TTCard()
      {
        card = new Sturdy()
      });
      extraTooltips.Add(new TTGlossary("cardtrait.retain", Array.Empty<object>()));
      return extraTooltips;
    }
  }
}
