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

namespace NukeDragon.TeamSnakemouth
{
  internal sealed class FrozenRenderTranspiler
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.RenderPartUI)), transpiler: new HarmonyMethod(typeof(FrozenRenderTranspiler), nameof(RenderTranspiler)));
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
            new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(FrozenRenderTranspiler), nameof(DrawFrozenIcon))),
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
  }
}
