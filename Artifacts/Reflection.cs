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
  internal class Reflection : Artifact, IArtifact
  {
    public Deck? AssignedDeck;
    public int counter = 0;
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("Reflection", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Kabbu_Deck.Deck,
          pools = [ArtifactPool.Common],
        },
        Sprite = ModEntry.Instance.ReflectionSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Reflection", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Reflection", "description"]).Localize
      });
    }

    public override void OnReceiveArtifact(State state)
    {
      state.GetCurrentQueue().QueueImmediate<CardAction>(new AArtifactChooseCharacter { artifact = this });
    }

    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
      if (deck == AssignedDeck)
      {
        counter++;
        if (counter >= 3)
        {
          counter = 0;
          combat.Queue(new AStatus
          {
            status = Status.shield,
            statusAmount = 1,
            targetPlayer = true,
            artifactPulse = this.Key(),
          });
        }
      }
    }

    public override int? GetDisplayNumber(State s)
    {
      return counter;
    }
  }
}
