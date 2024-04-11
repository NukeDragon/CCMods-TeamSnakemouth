using HarmonyLib;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth.Features
{
  internal sealed class InspirationManager
  {
    public InspirationManager() 
    {
      ModEntry.Instance.Helper.Events.RegisterAfterArtifactsHook(nameof(Artifact.ModifyBaseDamage), (int baseDamage, Card? card, State state, Combat combat, bool fromPlayer) =>
      {
        if (!fromPlayer) return 0;
        Deck? deck = card?.GetMeta().deck;
        Deck owner = deck.GetValueOrDefault();
        IStatusEntry? status;
        if (!ModEntry.Instance.Inspired_Status_Dictionary.TryGetValue(owner, out status)) return 0;
        return state.ship.Get(status.Status);

      }, 0);
    }
  }
}
