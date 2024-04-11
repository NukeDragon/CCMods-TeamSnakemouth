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
using FMOD;

namespace NukeDragon.TeamSnakemouth.Patches
{
  internal class FrozenIntentStunTranspiler
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(ANextEnemyIntent), nameof(ANextEnemyIntent.Begin)), transpiler: new HarmonyMethod(typeof(FrozenIntentStunTranspiler), nameof(BeginTranspiler)));
    }
    private static IEnumerable<CodeInstruction> BeginTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
    {
      try
      {
        Label label = ilGenerator.DefineLabel();
        return new SequenceBlockMatcher<CodeInstruction>(instructions).Find(
            ILMatches.Instruction(OpCodes.Ldnull).Anchor(out var anchor),
            ILMatches.Stfld("intent")
          ).Anchors().PointerMatcher(anchor).AddLabel(label).Find(
          SequenceBlockMatcherFindOccurence.First,
          SequenceMatcherRelativeBounds.WholeSequence,
            ILMatches.AnyStloc.CreateLdlocInstruction(out var index),
            ILMatches.AnyLdloc,
            ILMatches.Instruction(OpCodes.Ldc_I4_M1)
          ).Find(
            ILMatches.Instruction(OpCodes.Dup),
            ILMatches.Ldfld("intent"),
            ILMatches.Instruction(OpCodes.Dup),
            ILMatches.Brtrue
          ).Insert(
            SequenceMatcherPastBoundsDirection.Before,
            SequenceMatcherInsertionResultingBounds.IncludingInsertion,
            index,
            new CodeInstruction(OpCodes.Ldarg_2),
            new CodeInstruction(OpCodes.Ldarg_3),
            new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(FrozenIntentSkip))),
            new CodeInstruction(OpCodes.Brtrue, label)
          )
          .AllElements();
      }
      catch (Exception e)
      {
        Console.WriteLine("Team Snakemouth Transpiler 'FrozenIntentStunTranspiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
        Console.WriteLine(e);
        return instructions;
      }
    }
    private static bool FrozenIntentSkip(int index, State s, Combat c)
    {
      Part part = c.otherShip.parts[index];
      PDamMod pdm = part.GetDamageModifier();
      if (pdm != ModEntry.Instance.frozen || part.stunModifier == PStunMod.unstunnable) return false;
      foreach (Artifact artifact in s.EnumerateAllArtifacts())
      {
        if (artifact is ExtraFreeze) return false;
      }
      Audio.Play(new GUID?(FSPRO.Event.Status_Stun));
      return true;
    }
  }
}
