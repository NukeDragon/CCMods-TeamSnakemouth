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
using NukeDragon.TeamSnakemouth.Patches;
using FMOD;

namespace NukeDragon.TeamSnakemouth
{
  internal sealed class FrozenManager
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.ModifyDamageDueToParts)), transpiler: new HarmonyMethod(typeof(FrozenManager), nameof(ModifyDamageDueToPartsTranspiler)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.RenderPartUI)), transpiler: new HarmonyMethod(typeof(FrozenManager), nameof(RenderTranspiler)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(ANextEnemyIntent), nameof(ANextEnemyIntent.Begin)), transpiler: new HarmonyMethod(typeof(FrozenManager), nameof(BeginTranspiler)));
    }
    private static IEnumerable<CodeInstruction> RenderTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
    {
      try
      {
        var localVars = originalMethod.GetMethodBody()!.LocalVariables;

        return new SequenceBlockMatcher<CodeInstruction>(instructions).Find(
          ILMatches.Ldarg(3),
          ILMatches.AnyCall,
          ILMatches.AnyStloc.CreateLdlocInstruction(out var pdm)
          ).Find(
          ILMatches.AnyStloc.CreateLdlocInstruction(out var vec),
          ILMatches.LdcR8(0.5),
          ILMatches.LdcR8(0.5)
          ).Find(
          ILMatches.AnyLdloca.CreateLdlocInstruction(out var color),
          ILMatches.LdcR8(1),
          ILMatches.LdcR8(1),
          ILMatches.LdcR8(1),
          ILMatches.LdcR8(0.8)
          ).Find(
          ILMatches.Ldfld("stunModifier"),
          ILMatches.Instruction(OpCodes.Ldc_I4_1)
          ).Insert(
          SequenceMatcherPastBoundsDirection.Before,
          SequenceMatcherInsertionResultingBounds.IncludingInsertion,
          new List<CodeInstruction> { 
            pdm, 
            vec, 
            color,
            new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(FrozenManager), nameof(DrawFrozenIcon))),
            }
          
          )
          .AllElements();
      }
      catch (Exception e)
      {
        Console.WriteLine("Team Snakemouth Transpiler 'FrozenRenderTranspiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
        Console.WriteLine(e);
        return instructions;
      }
    }

    public static void DrawFrozenIcon(PDamMod pdm, Vec vec, Color color)
    {
      if (pdm == ModEntry.Instance.frozen)
      {
        Spr? id1 = new Spr?(ModEntry.Instance.Frozen_Modifier.Sprite);
        double x1 = vec.x;
        double y1 = vec.y;
        Color? nullable2 = new Color?(color);
        Vec? originPx1 = new Vec?();
        Vec? originRel1 = new Vec?();
        Vec? scale1 = new Vec?();
        Rect? pixelRect1 = new Rect?();
        Color? color3 = nullable2;
        Draw.Sprite(id1, x1, y1, originPx: originPx1, originRel: originRel1, scale: scale1, pixelRect: pixelRect1, color: color3);
      }
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

