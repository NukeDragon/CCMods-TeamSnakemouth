using Nickel;
using System.Reflection;
using System.Collections.Generic;
using FSPRO;
using HarmonyLib;
using System;
using Nanoray.Shrike.Harmony;
using Nanoray.Shrike;
using System.Reflection.Emit;
using System.Collections;

namespace NukeDragon.TeamSnakemouth
{
  internal sealed class ChargeManager
  {
    public ChargeManager()
    {
      ModEntry.Instance.Helper.Events.RegisterAfterArtifactsHook(nameof(Artifact.ModifyBaseDamage), (int baseDamage, Card? card, State state, Combat combat, bool fromPlayer) =>
      {
        if (!fromPlayer) return 0;
        Deck? deck = card?.GetMeta().deck;
        Deck owner = deck.GetValueOrDefault();
        IStatusEntry? status;
        if (!ModEntry.Instance.Charged_Status_Dictionary.TryGetValue(owner, out status)) return 0;
        return state.ship.Get(status.Status);

      }, 0);
    }

    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.Begin)), postfix: new HarmonyMethod(typeof(ChargeManager), nameof(AAttack_Postfix)));
    }
    private static void AAttack_Postfix(AAttack __instance, State s, Combat c)
    {
      if (__instance.whoDidThis == null) return;
      Deck owner = (Deck)__instance.whoDidThis;
      IStatusEntry? status;
      if (ModEntry.Instance.Charged_Status_Dictionary.TryGetValue(owner, out status)) {
        bool targetPlayer = __instance.targetPlayer;
        Ship ship = targetPlayer ? c.otherShip : s.ship;
        if (ship.Get(status.Status) > 0)
          c.QueueImmediate(new AStatus()
          {
            targetPlayer = !targetPlayer,
            mode = AStatusMode.Set,
            status = status.Status,
            statusAmount = 0,
          });
      }
    }

  }
}