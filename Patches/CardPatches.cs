using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace NukeDragon.TeamSnakemouth.Patches
{
  internal class CardPatches
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Card), nameof(Card.RenderAction)), prefix: new HarmonyMethod(typeof(CardPatches), nameof(CardRenderActionPrefix)));
    }
    private static bool CardRenderActionPrefix(G g, State state, CardAction action, bool dontDraw, int shardAvailable, int stunChargeAvailable, int bubbleJuiceAvailable, ref int __result)
    {
      if (action is ATPCostAction tpAction)
      {
        if (tpAction.action == null) return true;

        var position = g.Push(rect: new()).rect.xy;
        int initialX = (int)position.x;

        int tpcost = tpAction.cost;
        int tpamount = state.ship.Get(ModEntry.Instance.TP_Status.Status);

        int tpspacing = 6;

        int tpOneIcon = 0;
        int tpFiveIcon = 0;
        for (int i = 0; i < tpcost;)
        {
          if (i + 4 < tpcost)
          {
            tpFiveIcon++;
            i += 5;
          }
          else
          {
            tpOneIcon++;
            i++;
          }
        }
        int tpOnesAmount = tpamount - (tpFiveIcon * 5);
        if (tpOnesAmount < 1) tpOnesAmount = 0;
        int tpOnesFulfilled = 0;
        int tpOnesOff = 0;
        for (int i = 0; i < tpOneIcon; i++)
        {
          if (i < tpOnesAmount)
          {
            tpOnesFulfilled++;
          }
          else tpOnesOff++;
        }
        for (int i = 0; i < tpOnesOff; i++) 
        {
          if (!dontDraw) Draw.Sprite(ModEntry.Instance.TPCostOff.Sprite, position.x, position.y, color: tpAction.disabled ? Colors.disabledIconTint : null);
          position.x += tpspacing;
        }
        for (int i = 0; i < tpOnesFulfilled; i++)
        {
          if (!dontDraw) Draw.Sprite(ModEntry.Instance.TPCost.Sprite, position.x, position.y, color: tpAction.disabled ? Colors.disabledIconTint : null);
          position.x += tpspacing;
        }
        for (int i = 0; i < tpFiveIcon; i++)
        {
          if (!dontDraw)
          {
            if ((5 * (i+1)) - 1 < tpamount)
            {
              Draw.Sprite(ModEntry.Instance.FiveTPCost.Sprite, position.x, position.y, color: tpAction.disabled ? Colors.disabledIconTint : null);
            }
            else
            {
              Draw.Sprite(ModEntry.Instance.FiveTPCostOff.Sprite, position.x, position.y, color: tpAction.disabled ? Colors.disabledIconTint : null);
            }
          }
          position.x += tpspacing;
        }
        position.x += 2;
        g.Push(rect: new(position.x - initialX, 0));
        position.x += Card.RenderAction(g, state, tpAction.action, dontDraw, shardAvailable, stunChargeAvailable, bubbleJuiceAvailable);
        g.Pop();
        __result = (int)position.x - initialX;
        g.Pop();
        return false;
      }
      if (action is AAttack aattack)
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
            Draw.Sprite(ModEntry.Instance.FrozenModifierSprite.Sprite, position.x, position.y, color: aattack.disabled ? Colors.disabledIconTint : null);
          }
          position.x += SpriteLoader.Get(ModEntry.Instance.FrozenModifierSprite.Sprite)!.Width;
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
