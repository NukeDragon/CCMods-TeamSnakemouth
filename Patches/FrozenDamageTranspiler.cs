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

namespace NukeDragon.TeamSnakemouth.Patches
{
  internal class FrozenDamageTranspiler
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.ModifyDamageDueToParts)), transpiler: new HarmonyMethod(typeof(FrozenDamageTranspiler), nameof(ModifyDamageDueToPartsTranspiler)));
    }
    private static IEnumerable<CodeInstruction> ModifyDamageDueToPartsTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
    {
      try
      {
        return new SequenceBlockMatcher<CodeInstruction>(instructions).Find(
            ILMatches.Instruction(OpCodes.Switch),
            ILMatches.Br.GetBranchTarget(out var failBranchTarget),
            ILMatches.Ldarg(3),
            ILMatches.AnyStloc.CreateLdlocaInstruction(out var ldlocanum)
          )
          .PointerMatcher(failBranchTarget)
          .Advance(-1)
          .GetBranchTarget(out var successBranchTarget)
          .PointerMatcher(failBranchTarget)
          .ExtractLabels(out var labels)
          .Insert(
            SequenceMatcherPastBoundsDirection.Before,
            SequenceMatcherInsertionResultingBounds.IncludingInsertion,
            new CodeInstruction(OpCodes.Ldarg, 3).WithLabels(labels),
            new CodeInstruction(OpCodes.Ldarg, 4),
            ldlocanum,
            new CodeInstruction(OpCodes.Ldarg_1),
            new CodeInstruction(OpCodes.Ldarg_2),
            new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(FrozenDamageModify))),
            new CodeInstruction(OpCodes.Brtrue, successBranchTarget)
          )
          .AllElements();
      }
      catch (Exception e)
      {
        Console.WriteLine("Team Snakemouth Transpiler 'FrozenDamageTranspiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
        Console.WriteLine(e);
        return instructions;
      }
    }
    private static bool FrozenDamageModify(int incomingDamage, Part part, ref int num, State s, Combat c)
    {
      PDamMod pdm = part.GetDamageModifier();
      if (pdm != ModEntry.Instance.frozen) { return false; }
      bool hasFrostbite = false;
      Ship? ship = null;
      int offset = 0;
      for (int i = 0; i < c.otherShip.parts.Count; ++i)
      {
        Part part2 = c.otherShip.parts[i];
        if (part == part2) { ship = c.otherShip; offset = i; break; }
      }
      foreach (Part part2 in s.ship.parts)
      {
        if (part == part2) { ship = s.ship; break; }
      }

      foreach (Artifact artifact in s.EnumerateAllArtifacts())
      {
        if (artifact is Frostbite) hasFrostbite = true;
        if (artifact is ExtraFreeze)
        {
          if (ship == c.otherShip)
          {
            c.QueueImmediate(new AStunPart()
            {
              worldX = c.otherShip.x + offset
            });
          }
        }
      }
      if (hasFrostbite) num = incomingDamage * 2;
      else num = incomingDamage + 1;
      part.SetOverride2(new PDamMod?());
      return true;
    }

  }
}
