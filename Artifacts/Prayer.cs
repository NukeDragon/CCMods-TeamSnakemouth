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
  internal class Prayer : Artifact, IArtifact
  {
    public Deck? AssignedDeck;
    public int counter = 0;
    public bool onceperbattle = true;
    public static void Register(IModHelper helper)
    {
      helper.Content.Artifacts.RegisterArtifact("Prayer", new()
      {
        ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          owner = ModEntry.Instance.Kabbu_Deck.Deck,
          pools = [ArtifactPool.Common],
        },
        Sprite = ModEntry.Instance.PrayerSprite.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Prayer", "name"]).Localize,
        Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Prayer", "description"]).Localize
      });
    }

    public override void OnReceiveArtifact(State state)
    {
      state.GetCurrentQueue().QueueImmediate<CardAction>(new AArtifactChooseCharacter { artifact = this });
    }

    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
      if (deck == AssignedDeck && onceperbattle)
      {
        counter++;
        if (counter >= 10)
        {
          counter = 0;
          onceperbattle = false;
          combat.Queue(new AHeal 
          {
            healAmount = 2,
            canRunAfterKill = true,
            artifactPulse = this.Key(),
            targetPlayer = true
          });
        }
      }
    }
    public override void OnCombatStart(State state, Combat combat)
    {
      counter = 0;
      onceperbattle = true;
    }

    public override int? GetDisplayNumber(State s)
    {
      if (onceperbattle) return counter;
      return null;
    }
  }
}
