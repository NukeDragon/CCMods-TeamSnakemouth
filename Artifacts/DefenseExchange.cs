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
    public Deck? AssignedDeck;
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
    public override void OnReceiveArtifact(State state)
    {
      state.GetCurrentQueue().QueueImmediate<CardAction>(new AArtifactChooseCharacter { artifact = this });
    }

    public override int ModifyBaseDamage(int baseDamage, Card? card, State state, Combat? combat, bool fromPlayer)
    {
      if (card != null)
        if (card.GetMeta().deck == AssignedDeck) return -1;
      return 0;
    }

    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
      if (card.GetMeta().deck == AssignedDeck)
      {
        combat.QueueImmediate(new AStatus()
        {
          status = Status.tempShield,
          statusAmount = 1,
          artifactPulse = this.Key(),
          targetPlayer = true
        });
      }
    }
  }
}
