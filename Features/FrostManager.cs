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
using NukeDragon.TeamSnakemouth.Patches;

namespace NukeDragon.TeamSnakemouth
{
  internal class FrostManager
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnBeginTurn)), postfix: new HarmonyMethod(typeof(FrostManager), nameof(OnBeginTurn_Postfix)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(AStatus), nameof(AStatus.Begin)), prefix: new HarmonyMethod(typeof(FrostManager), nameof(Begin_Prefix)));
    }
    public static void OnBeginTurn_Postfix(Combat c, Ship __instance)
    {
      int heatCount = __instance.Get(Status.heat);
      int frostCount = __instance.Get(ModEntry.Instance.Frost_Status.Status);
      if (heatCount > 0 && frostCount > 0)
      {
        if (__instance == c.otherShip)
        {
          c.QueueImmediate(new AStatus()
          {
            targetPlayer = false,
            status = Status.heat,
            statusAmount = -1,
            timer = 0
          });
          c.QueueImmediate(new AStatus()
          {
            targetPlayer = false,
            status = ModEntry.Instance.Frost_Status.Status,
            statusAmount = -1,
            timer = 0
          });
        }
        else
        {
          c.QueueImmediate(new AStatus()
          {
            targetPlayer = true,
            status = Status.heat,
            statusAmount = -1,
            timer = 0
          });
          c.QueueImmediate(new AStatus()
          {
            targetPlayer = true,
            status = ModEntry.Instance.Frost_Status.Status,
            statusAmount = -1,
            timer = 0
          });
        }
      }
    }
    public static bool Begin_Prefix(AStatus __instance, State s, Combat c)
    {
      Ship ship = __instance.targetPlayer ? s.ship : c.otherShip;
      if (ship == null || ship.hull <= 0)
        return true;
      if (ship.Get(ModEntry.Instance.Concentration_Status.Status) > 0 && __instance.status == ModEntry.Instance.Frost_Status.Status && __instance.statusAmount > 0)
      {
        AStatus a = new AStatus();
        a.status = ModEntry.Instance.Concentration_Status.Status;
        a.statusAmount = -1;
        a.targetPlayer = __instance.targetPlayer;
        a.timer = 0.0;
        c.QueueImmediate((CardAction)a);
        return false;
      }
      return true;
    }
  }
}
