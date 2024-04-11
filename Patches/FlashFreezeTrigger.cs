using Nickel;
using System.Reflection;
using System.Collections.Generic;
using FSPRO;
using HarmonyLib;

namespace NukeDragon.TeamSnakemouth
{
  internal sealed class FlashFreezeTrigger
  {
    public FlashFreezeTrigger()
    {
      ModEntry.Instance.Helper.Events.RegisterAfterArtifactsHook(nameof(Artifact.OnQueueEmptyDuringPlayerTurn), (State state, Combat combat) =>
      {
        if (state.ship.Get(ModEntry.Instance.Frost_Status.Status) >= 3) 
        {
          combat.QueueImmediate(new AFlashFreeze()
          {
            targetPlayer = true
          });
        }
        if (combat.otherShip.Get(ModEntry.Instance.Frost_Status.Status) >= 3)
        {
          combat.QueueImmediate(new AFlashFreeze() 
          { 
            targetPlayer = false 
          });
        }
      }, 0);
    }
  }
}