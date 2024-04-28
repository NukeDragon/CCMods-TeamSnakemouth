using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Nanoray.Shrike;
using Nanoray.Shrike.Harmony;
using System.Data.SqlTypes;
using Nickel;
using static System.Collections.Specialized.BitVector32;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace NukeDragon.TeamSnakemouth
{
  internal static class FreezeAttackExt
  {
    public static bool? GetFreezing(this AAttack self) => ModEntry.Instance.Helper.ModData.GetOptionalModData<bool>(self, "freezing");
    public static void SetFreezing(this AAttack self, bool value) => ModEntry.Instance.Helper.ModData.SetOptionalModData<bool>(self, "freezing", value);
    public static Status? GetStatus2(this AAttack self) => ModEntry.Instance.Helper.ModData.GetOptionalModData<Status>(self, "status2");
    public static void SetStatus2(this AAttack self, Status value) => ModEntry.Instance.Helper.ModData.SetOptionalModData<Status>(self, "status2", value);
    public static int? GetStatus2Amount(this AAttack self) => ModEntry.Instance.Helper.ModData.GetOptionalModData<int>(self, "status2amount");
    public static void SetStatus2Amount(this AAttack self, int value) => ModEntry.Instance.Helper.ModData.SetOptionalModData<int>(self, "status2amount", value);
    public static Deck? GetCard(this AAttack self) => ModEntry.Instance.Helper.ModData.GetOptionalModData<Deck>(self, "deck");
    public static void SetCard(this AAttack self, Deck value) => ModEntry.Instance.Helper.ModData.SetOptionalModData<Deck>(self, "deck", value);
  }
  internal class AAttackPatches
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.Begin)), transpiler: new HarmonyMethod(typeof(AAttackPatches), nameof(AAttackModifyHookTranspiler)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.Begin)), transpiler: new HarmonyMethod(typeof(AAttackPatches), nameof(AttackModifierApplyTranspiler)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.GetTooltips)), postfix: new HarmonyMethod(typeof(AAttackPatches), nameof(GetTooltipsPostfix)));
    }

    private static List<Tooltip> GetTooltipsPostfix(List<Tooltip> __result, AAttack __instance)
    {
      List<Tooltip> result = __result;
      if (__instance.GetFreezing() == true)
      {
        foreach (Tooltip tooltip in FrozenManager.GetTooltips())
        {
          result.Add(tooltip);
        }
      };
      return result;
    } 
    
    private static IEnumerable<CodeInstruction> AAttackModifyHookTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
    {
      try
      {
        return new SequenceBlockMatcher<CodeInstruction>(instructions).Find(
          ILMatches.Ldarg(2),
          ILMatches.AnyCall,
          ILMatches.AnyCall,
          ILMatches.AnyStloc
          ).Insert(
          SequenceMatcherPastBoundsDirection.Before,
          SequenceMatcherInsertionResultingBounds.IncludingInsertion,
          new CodeInstruction(OpCodes.Ldarg_0),
          new CodeInstruction(OpCodes.Ldarg_2),
          new CodeInstruction(OpCodes.Ldarg_3),
          new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AAttackModifyHook)))
          )
          .AllElements();
      }
      catch (Exception e)
      {
        Console.WriteLine("Team Snakemouth Transpiler 'AAttackPatches.AAttackModifyHookTranspiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
        Console.WriteLine(e);
        return instructions;
      }
    }
    private static void AAttackModifyHook(AAttack self, State s, Combat c)
    {
      foreach (var hook in ModEntry.Instance.HookManager.GetHooksWithProxies(ModEntry.Instance.KokoroApi, s.EnumerateAllArtifacts())) hook.ModifyAAttack(ref self, s, c);
    }

      private static IEnumerable<CodeInstruction> AttackModifierApplyTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
    {
      try
      {
        return new SequenceBlockMatcher<CodeInstruction>(instructions).Find(
            ILMatches.AnyCall,
            ILMatches.AnyCall,
            ILMatches.AnyStloc.CreateLdlocInstruction(out var ray),
            ILMatches.AnyLdloc
          ).Find(
          ILMatches.Ldfld("armorize"),
          ILMatches.Brfalse.GetBranchTarget(out var armorizeattackfalselabel)
          ).Find(
          ILMatches.AnyLdloc,
          ILMatches.Brfalse,
          ILMatches.Ldarg(0),
          ILMatches.Ldarg(3),
          ILMatches.AnyLdloc,
          ILMatches.AnyCall,
          ILMatches.Ldarg(0)
          ).Insert(
            SequenceMatcherPastBoundsDirection.Before,
            SequenceMatcherInsertionResultingBounds.IncludingInsertion,
            new CodeInstruction(OpCodes.Ldarg, 3).WithLabels(armorizeattackfalselabel),
            ray,
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AttackModifierApply)))
          )
          .AllElements();
      }
      catch (Exception e)
      {
        Console.WriteLine("Team Snakemouth Transpiler 'AAttackPatches.AttackModifierApplyTranspiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
        Console.WriteLine(e);
        return instructions;
      }
    }
    private static void AttackModifierApply(Combat c, RaycastResult ray, AAttack self)
    {
      if (self.GetFreezing() == true)
      {
        c.QueueImmediate(new AFreeze()
        {
          targetPlayer = self.targetPlayer,
          worldX = ray.worldX
        }); ;
      }
      if (!self.targetPlayer && c.otherShip.Get(ModEntry.Instance.Taunted_Status.Status) > 0 && !self.stunEnemy)
      {
        c.QueueImmediate(new AStunPart()
        {
          worldX = ray.worldX,
        });
      }
      if (self.GetStatus2().HasValue)
      {
        c.QueueImmediate(new AStatus()
        {
          status = self.GetStatus2().GetValueOrDefault(),
          statusAmount = self.GetStatus2Amount().GetValueOrDefault(),
          targetPlayer = self.targetPlayer,
        });
      }
    }
  }
}
