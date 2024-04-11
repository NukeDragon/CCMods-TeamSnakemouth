using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Nanoray.Shrike.Harmony;
using Nanoray.Shrike;
using Nickel;
using Microsoft.Xna.Framework;

namespace NukeDragon.TeamSnakemouth.Features
{
  internal static class IceMissileExt
  {
    public static bool? GetFreezing(this AMissileHit self) => ModEntry.Instance.Helper.ModData.GetOptionalModData<bool>(self, "freezing");
    public static void SetFreezing(this AMissileHit self, bool value) => ModEntry.Instance.Helper.ModData.SetOptionalModData<bool>(self, "freezing", value);
  }
  internal class IceMissileManager
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Missile), nameof(Missile.GetActions)), postfix: new HarmonyMethod(typeof(IceMissileManager), nameof(IceMissileManager.Missile_GetActions_Postfix)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(AMissileHit), nameof(AMissileHit.Update)), transpiler: new HarmonyMethod(typeof(IceMissileManager), nameof(IceMissileManager.AMissileHit_Transpiler)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Missile), nameof(Missile.Render)), transpiler: new HarmonyMethod(typeof(IceMissileManager), nameof(IceMissileManager.Missile_ExhaustRemover_Transpiler)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Missile), nameof(Missile.Render)), transpiler: new HarmonyMethod(typeof(IceMissileManager), nameof(IceMissileManager.Missile_GlowRemover_Transpiler)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Missile), nameof(Missile.GetTooltips)), postfix: new HarmonyMethod(typeof(IceMissileManager), nameof(IceMissileManager.Missile_GetTooltips_Postfix)));
    }

    private static List<CardAction>? Missile_GetActions_Postfix(List<CardAction>? __result, Missile __instance)
    {
      if (__instance.missileType != ModEntry.Instance.ice) return __result;
      List<CardAction>? cardactionlist = new List<CardAction>();
      AMissileHit action = new AMissileHit()
      {
        worldX = __instance.x,
        outgoingDamage = Missile.missileData[__instance.missileType].baseDamage,
        targetPlayer = __instance.targetPlayer
      };
      action.SetFreezing(true);
      cardactionlist.Add(action);
      return cardactionlist;
    }

    private static IEnumerable<CodeInstruction> AMissileHit_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
    {
      try
      {
        return new SequenceBlockMatcher<CodeInstruction>(instructions).Find(
          ILMatches.Ldfld("weaken"),
          ILMatches.AnyLdloc,
          ILMatches.Instruction(OpCodes.And),
          ILMatches.Brfalse.GetBranchTarget(out var weakenfalselabel)
          ).Find(
          ILMatches.AnyLdloc,
          ILMatches.LdcI4(43),
          ILMatches.AnyCall,
          ILMatches.AnyLdcI4
          ).Insert(
            SequenceMatcherPastBoundsDirection.Before,
            SequenceMatcherInsertionResultingBounds.IncludingInsertion,
            new CodeInstruction(OpCodes.Ldarg_3).WithLabels(weakenfalselabel),
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(MissileModifierApply)))
          )
          .AllElements();
      }
      catch (Exception e)
      {
        Console.WriteLine("Team Snakemouth Transpiler 'AMissileHit_Transpiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
        Console.WriteLine(e);
        return instructions;
      }
    }

    private static void MissileModifierApply(Combat c, AMissileHit self)
    {
      if (self.GetFreezing() == true)
      {
        c.QueueImmediate(new AFreeze()
        {
          targetPlayer = self.targetPlayer,
          worldX = self.worldX
        });
      }
    }

    private static IEnumerable<CodeInstruction> Missile_ExhaustRemover_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
    {
      try
      {
        return new SequenceBlockMatcher<CodeInstruction>(instructions).Find(
          ILMatches.AnyStloc,
          ILMatches.AnyLdarg,
          ILMatches.Ldfld("skin"),
          ILMatches.Brtrue.GetBranchTarget(out var targetlabel)
          ).Insert(
            SequenceMatcherPastBoundsDirection.After,
            SequenceMatcherInsertionResultingBounds.IncludingInsertion,
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(ReallySmallMethod))),
            new CodeInstruction(OpCodes.Brtrue, targetlabel.Value)
          )
          .AllElements();
      }
      catch (Exception e)
      {
        Console.WriteLine("Team Snakemouth Transpiler 'Missile_ExhaustRemover_Transpiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
        Console.WriteLine(e);
        return instructions;
      }
    }

    private static bool ReallySmallMethod(Missile self)
    {
      return self.missileType == ModEntry.Instance.ice;
    }
    private static IEnumerable<CodeInstruction> Missile_GlowRemover_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
    {
      try
      {
        return new SequenceBlockMatcher<CodeInstruction>(instructions).Find(
          ILMatches.AnyCall,
          ILMatches.AnyLdarg,
          ILMatches.Ldfld("skin"),
          ILMatches.Brtrue.GetBranchTarget(out var targetlabel)
          ).Insert(
            SequenceMatcherPastBoundsDirection.After,
            SequenceMatcherInsertionResultingBounds.IncludingInsertion,
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(ReallySmallMethod))),
            new CodeInstruction(OpCodes.Brtrue, targetlabel.Value)
          )
          .AllElements();
      }
      catch (Exception e)
      {
        Console.WriteLine("Team Snakemouth Transpiler 'Missile_GlowRemover_Transpiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
        Console.WriteLine(e);
        return instructions;
      }
    }
    private static List<Tooltip> Missile_GetTooltips_Postfix(List<Tooltip> __result, Missile __instance)
    {
      if (__instance.missileType != ModEntry.Instance.ice) return __result;
      List<Tooltip> tooltips =
      [
        new CustomTTGlossary(CustomTTGlossary.GlossaryType.midrow, () => ModEntry.Instance.Icicle_Icon.Sprite, () => ModEntry.Instance.Localizations.Localize(["tooltips", "iceMissile", "name"]), () => ModEntry.Instance.Localizations.Localize(["tooltips", "iceMissile", "description"]), key: $"{ModEntry.Instance.Package.Manifest.UniqueName}::IceMissile"),
      ];
      return tooltips;
    }
  }
}
