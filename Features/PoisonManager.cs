using Nickel;
using System.Reflection;
using System.Collections.Generic;
using FSPRO;
using HarmonyLib;

namespace NukeDragon.TeamSnakemouth
{
  internal sealed class PoisonManager
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnAfterTurn)), postfix: new HarmonyMethod(typeof(PoisonManager), nameof(OnAfterTurn_Postfix)));
    }
    private static void OnAfterTurn_Postfix(Ship __instance, State s, Combat c)
    {
      if (__instance.Get(ModEntry.Instance.Poison_Status.Status) > 0)
      {
        bool hasEternalVenom = false;
        c.Queue(new AHurt()
        {
          hurtAmount = __instance.Get(ModEntry.Instance.Poison_Status.Status),
          targetPlayer = __instance == c.otherShip ? false : true,
          cannotKillYou = true,
          hurtShieldsFirst = true
        });
        foreach (Artifact artifact in s.EnumerateAllArtifacts())
        {
          if (artifact is EternalVenom) hasEternalVenom = true;
        }
        if (!hasEternalVenom || __instance.Get(Status.timeStop) > 0)
        {
          c.Queue(new AStatus()
          {
            status = ModEntry.Instance.Poison_Status.Status,
            statusAmount = -1,
            targetPlayer = __instance == c.otherShip ? false : true,
          });
        }

      }
    }
  }
}