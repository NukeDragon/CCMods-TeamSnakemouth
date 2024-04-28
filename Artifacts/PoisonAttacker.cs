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
  internal class PoisonAttacker : Artifact, IArtifact
  {
    public Deck? AssignedDeck;
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("PoisonAttacker", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Vi_Deck.Deck,
          pools = [ArtifactPool.Common],
        },
        Sprite = ModEntry.Instance.PoisonAttackerSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "PoisonAttacker", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "PoisonAttacker", "description"]).Localize
      });
    }

    public override void OnReceiveArtifact(State state)
    {
      state.GetCurrentQueue().QueueImmediate<CardAction>(new AArtifactChooseCharacter { artifact = this });
    }

    public override int ModifyBaseDamage(int baseDamage, Card? card, State state, Combat? combat, bool fromPlayer)
    {
      if (!fromPlayer) return 0;
      if (state.ship.Get(ModEntry.Instance.Poison_Status.Status) < 1) return 0;
      Deck? deck = card?.GetMeta().deck;
      Deck? owner = this.AssignedDeck;
      return deck == owner ? 2 : 0;
    }

    public override List<Tooltip>? GetExtraTooltips() => StatusMeta.GetTooltips(ModEntry.Instance.Poison_Status.Status, 1);
  }
}
