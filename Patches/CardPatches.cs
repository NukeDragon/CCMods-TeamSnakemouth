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

        int tpspacing = tpcost >= 5 ? 5 : 6;

        for (int i = 0; i < tpcost; i++)
        {
          if (!dontDraw)
          {
            if (i < tpamount)
            {
              Draw.Sprite(ModEntry.Instance.TPCost.Sprite, position.x, position.y);
            }
            else
            {
              Draw.Sprite(ModEntry.Instance.TPCostOff.Sprite, position.x, position.y);
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
            Draw.Sprite(ModEntry.Instance.FrozenModifierSprite.Sprite, position.x, position.y);
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
