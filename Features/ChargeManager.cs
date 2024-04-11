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
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Card), nameof(Card.GetActualDamage)), transpiler: new HarmonyMethod(typeof(ChargeManager), nameof(ActualDamageTranspiler)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.Begin)), postfix: new HarmonyMethod(typeof(ChargeManager), nameof(AAttack_Postfix)));
    }
    private static IEnumerable<CodeInstruction> ActualDamageTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
    {
      try
      {
        return new SequenceBlockMatcher<CodeInstruction>(instructions).Find(
            ILMatches.AnyLdloc,
            ILMatches.AnyStloc.CreateLdlocInstruction(out var ship),
            ILMatches.AnyLdloc,
            ILMatches.Brtrue
          ).Find(
            ILMatches.AnyLdloc.CreateLdlocaInstruction(out var actualdamage),
            ILMatches.LdcI4(0),
            ILMatches.Bge
          ).Insert(
            SequenceMatcherPastBoundsDirection.Before,
            SequenceMatcherInsertionResultingBounds.IncludingInsertion,
            ship,
            actualdamage,
            new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(ChargeDamageModify)))
          )
          .AllElements();
      }
      catch (Exception e)
      {
        Console.WriteLine("Team Snakemouth Transpiler 'ChargeManager.ActualDamageTranspiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
        Console.WriteLine(e);
        return instructions;
      }
    }
    private static void ChargeDamageModify(Ship ship, ref int actualdamage)
    {
      int num = ship.Get(ModEntry.Instance.Charge_Status.Status);
      actualdamage += num;
    }
    private static void AAttack_Postfix(AAttack __instance, State s, Combat c)
    {
      bool targetPlayer = __instance.targetPlayer;
      Ship ship = targetPlayer ? c.otherShip : s.ship;
      if (ship.Get(ModEntry.Instance.Charge_Status.Status) > 0)
      c.QueueImmediate(new AStatus()
      {
        targetPlayer = !targetPlayer,
        mode = AStatusMode.Set,
        status = ModEntry.Instance.Charge_Status.Status,
        statusAmount = 0,
      });
    }

  }
}