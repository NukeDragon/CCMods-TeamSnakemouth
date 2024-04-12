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
      harmony.Patch(AccessTools.DeclaredMethod(typeof(AAttack), nameof(AAttack.Begin)), transpiler: new HarmonyMethod(typeof(AAttackPatches), nameof(BeginTranspiler)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Card), nameof(Card.RenderAction)), prefix: new HarmonyMethod(typeof(AAttackPatches), nameof(CardRenderActionPrefix)));
    }
    private static IEnumerable<CodeInstruction> BeginTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
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
        Console.WriteLine("Team Snakemouth Transpiler 'AAttackModifierTranspiler.BeginTranspiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
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
    }
    private static IEnumerable<CodeInstruction> RenderTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator, MethodBase originalMethod)
    {
      try
      {
        CodeInstruction targetInstr = null!;
        return new SequenceBlockMatcher<CodeInstruction>(instructions).Find(
          ILMatches.AnyLdloca.CreateLdlocInstruction(out var loc).CreateLdlocaInstruction(out var loca),
          ILMatches.LdcI4(0),
          ILMatches.Stfld("w").WithDelegate((matcher, _, instruction) => { targetInstr = instruction; return matcher; })
          ).Find(
          ILMatches.AnyLdloc.CreateLdlocInstruction(out var aattack).ExtractLabels(out var label),
          ILMatches.Ldfld("moveEnemy"),
          ILMatches.LdcI4(0)
          ).Insert(
            SequenceMatcherPastBoundsDirection.Before,
            SequenceMatcherInsertionResultingBounds.IncludingInsertion,
            new CodeInstruction(OpCodes.Ldarg_1).WithLabels(label),
            aattack,
            new CodeInstruction(OpCodes.Ldarg_2),
            loca,
            new CodeInstruction(OpCodes.Ldflda, ((FieldInfo)targetInstr.operand).DeclaringType!.GetField("w")),
            loca,
            new CodeInstruction(OpCodes.Ldflda, ((FieldInfo)targetInstr.operand).DeclaringType!.GetField("isFirst")),
            new CodeInstruction(OpCodes.Ldarg, 4),
            new CodeInstruction(OpCodes.Ldarg_3),
            loc,
            new CodeInstruction(OpCodes.Ldfld, ((FieldInfo)targetInstr.operand).DeclaringType!.GetField("iconNumberPadding")),
            loc,
            new CodeInstruction(OpCodes.Ldfld, ((FieldInfo)targetInstr.operand).DeclaringType!.GetField("iconWidth")),
            loc,
            new CodeInstruction(OpCodes.Ldfld, ((FieldInfo)targetInstr.operand).DeclaringType!.GetField("numberWidth")),
            new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AttackModifierRender)))
          )
          .AllElements();
      }
      catch (Exception e)
      {
        Console.WriteLine("Team Snakemouth Transpiler 'AAttackModifierTranspiler.RenderTranspiler' failed. Ping @NukeDragon on discord to get him to fix this crap.");
        Console.WriteLine(e);
        return instructions;
      }
    }
    private static void AttackModifierRender(G gg, AAttack aattack, State state, ref int w, ref bool isFirst, bool dontDraw, CardAction action, int iconNumberPadding, int iconWidth, int numberWidth)
    {
      //bool freezing = aattack.GetFreezing().GetValueOrDefault();
      //if (freezing) 
      //{
        int? amount = null;
        bool flipY = false;
        int? x = null;
        Color spriteColor = action.disabled ? Colors.disabledIconTint : new Color("ffffff");
        if (!isFirst)
          w += 4;
        Rect? nullable1;
        if (!dontDraw)
        {
          G g = gg;
          nullable1 = new Rect?(new Rect((double)w));
          global::UIKey? key = new global::UIKey?();
          Rect? rect = nullable1;
          Rect? rectForReticle = new Rect?();
          global::UIKey? rightHint = new global::UIKey?();
          global::UIKey? leftHint = new global::UIKey?();
          global::UIKey? upHint = new global::UIKey?();
          global::UIKey? downHint = new global::UIKey?();
          Vec xy = g.Push(key, rect, rectForReticle, rightHint: rightHint, leftHint: leftHint, upHint: upHint, downHint: downHint).rect.xy;
          Spr? id = new Spr?(ModEntry.Instance.Frozen_Modifier.Sprite);
          double x1 = xy.x;
          double y = xy.y;
          int num = flipY ? 1 : 0;
          Color? nullable2 = new Color?(spriteColor);
          Vec? originPx = new Vec?();
          Vec? originRel = new Vec?();
          Vec? scale = new Vec?();
          nullable1 = new Rect?();
          Rect? pixelRect = nullable1;
          Color? color = nullable2;
          Draw.Sprite(id, x1, y, flipY: num != 0, originPx: originPx, originRel: originRel, scale: scale, pixelRect: pixelRect, color: color);
          g.Pop();
        }
        w += iconWidth;
        Color? nullable3;
        if (amount.HasValue)
        {
          int valueOrDefault = amount.GetValueOrDefault();
          if (!x.HasValue)
          {
            w += iconNumberPadding;
            string str = DB.IntStringCache(valueOrDefault);
            if (!dontDraw)
            {
              G g = gg;
              nullable1 = new Rect?(new Rect((double)w));
              global::UIKey? key = new global::UIKey?();
              Rect? rect = nullable1;
              Rect? rectForReticle = new Rect?();
              global::UIKey? rightHint = new global::UIKey?();
              global::UIKey? leftHint = new global::UIKey?();
              global::UIKey? upHint = new global::UIKey?();
              global::UIKey? downHint = new global::UIKey?();
              Vec xy = g.Push(key, rect, rectForReticle, rightHint: rightHint, leftHint: leftHint, upHint: upHint, downHint: downHint).rect.xy;
              int number = valueOrDefault;
              double x2 = xy.x;
              double y = xy.y;
              nullable3 = Colors.redd;
              Color color = nullable3 ?? Colors.textMain;
              BigNumbers.Render(number, x2, y, color);
              g.Pop();
            }
            w += str.Length * numberWidth;
          }
        }
        if (x.HasValue)
        {
          int? nullable4 = x;
          int num = 0;
          if (nullable4.GetValueOrDefault() < num & nullable4.HasValue)
          {
            w += iconNumberPadding;
            if (!dontDraw)
            {
              G g = gg;
              nullable1 = new Rect?(new Rect((double)(w - 2)));
              global::UIKey? key = new global::UIKey?();
              Rect? rect = nullable1;
              Rect? rectForReticle = new Rect?();
              global::UIKey? rightHint = new global::UIKey?();
              global::UIKey? leftHint = new global::UIKey?();
              global::UIKey? upHint = new global::UIKey?();
              global::UIKey? downHint = new global::UIKey?();
              Vec xy = g.Push(key, rect, rectForReticle, rightHint: rightHint, leftHint: leftHint, upHint: upHint, downHint: downHint).rect.xy;
              Spr? id = new Spr?(Spr.icons_minus);
              double x3 = xy.x;
              double y = xy.y - 1.0;
              nullable3 = action.disabled ? new Color?(spriteColor) : Colors.redd;
              Vec? originPx = new Vec?();
              Vec? originRel = new Vec?();
              Vec? scale = new Vec?();
              nullable1 = new Rect?();
              Rect? pixelRect = nullable1;
              Color? color = nullable3;
              Draw.Sprite(id, x3, y, originPx: originPx, originRel: originRel, scale: scale, pixelRect: pixelRect, color: color);
              g.Pop();
            }
            w += 3;
          }
          if (Math.Abs(x.Value) > 1)
          {
            w += iconNumberPadding + 1;
            if (!dontDraw)
            {
              G g = gg;
              nullable1 = new Rect?(new Rect((double)w));
              global::UIKey? key = new global::UIKey?();
              Rect? rect = nullable1;
              Rect? rectForReticle = new Rect?();
              global::UIKey? rightHint = new global::UIKey?();
              global::UIKey? leftHint = new global::UIKey?();
              global::UIKey? upHint = new global::UIKey?();
              global::UIKey? downHint = new global::UIKey?();
              Vec xy = g.Push(key, rect, rectForReticle, rightHint: rightHint, leftHint: leftHint, upHint: upHint, downHint: downHint).rect.xy;
              int number = Math.Abs(x.Value);
              double x4 = xy.x;
              double y = xy.y;
              nullable3 = Colors.redd;
              Color color = nullable3 ?? Colors.textMain;
              BigNumbers.Render(number, x4, y, color);
              g.Pop();
            }
            w += 4;
          }
          w += iconNumberPadding;
          if (!dontDraw)
          {
            G g = gg;
            nullable1 = new Rect?(new Rect((double)w));
            global::UIKey? key = new global::UIKey?();
            Rect? rect = nullable1;
            Rect? rectForReticle = new Rect?();
            global::UIKey? rightHint = new global::UIKey?();
            global::UIKey? leftHint = new global::UIKey?();
            global::UIKey? upHint = new global::UIKey?();
            global::UIKey? downHint = new global::UIKey?();
            Vec xy = g.Push(key, rect, rectForReticle, rightHint: rightHint, leftHint: leftHint, upHint: upHint, downHint: downHint).rect.xy;
            Spr? id = new Spr?(Spr.icons_x_white);
            double x5 = xy.x;
            double y = xy.y - 1.0;
            Icon? icon1 = action.GetIcon(state);
            ref Icon? local = ref icon1;
            nullable3 = local.HasValue ? new Color?(local.GetValueOrDefault().color) : new Color?();
            Vec? originPx = new Vec?();
            Vec? originRel = new Vec?();
            Vec? scale = new Vec?();
            nullable1 = new Rect?();
            Rect? pixelRect = nullable1;
            Color? color = nullable3;
            Draw.Sprite(id, x5, y, originPx: originPx, originRel: originRel, scale: scale, pixelRect: pixelRect, color: color);
            g.Pop();
          }
          w += 8;
        }
        isFirst = false;
      //}
    }
    private static bool CardRenderActionPrefix(G g, State state, CardAction action, bool dontDraw, int shardAvailable, int stunChargeAvailable, int bubbleJuiceAvailable, ref int __result)
    {
      if (action is AAttack aattack )
      {
        bool freezing = aattack.GetFreezing().GetValueOrDefault();
        if (freezing)
        {
          aattack.SetFreezing(false);

          var position = g.Push(rect: new()).rect.xy;
          int initialX = (int)position.x;

          position.x += Card.RenderAction(g, state, action, dontDraw, shardAvailable, stunChargeAvailable, bubbleJuiceAvailable);
          position.x += 2;
          if (!dontDraw)
          {
            Draw.Sprite(ModEntry.Instance.Frozen_Modifier.Sprite, position.x, position.y);
            }
          position.x += SpriteLoader.Get(ModEntry.Instance.Frozen_Modifier.Sprite)!.Width;
          __result = (int)position.x - initialX;
          aattack.SetFreezing(true);
          g.Pop();
          return false;
        }
      }
      return true;
    }
  }
}
