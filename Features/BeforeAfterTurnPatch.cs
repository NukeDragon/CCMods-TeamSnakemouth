using Nickel;
using System.Reflection;
using System.Collections.Generic;
using FSPRO;
using HarmonyLib;

namespace NukeDragon.TeamSnakemouth
{
  internal sealed class BeforeAfterTurnPatch
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnAfterTurn)), postfix: new HarmonyMethod(typeof(BeforeAfterTurnPatch), nameof(OnAfterTurn_Postfix)));
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnBeginTurn)), postfix: new HarmonyMethod(typeof(BeforeAfterTurnPatch), nameof(OnBeginTurn_Postfix)));
    }
    private static void OnAfterTurn_Postfix(Ship __instance, State s, Combat c)
    {
      if (__instance == c.otherShip)
      {
        bool flag = true;
        foreach (Artifact artifact in s.EnumerateAllArtifacts())
        {
          if (artifact is ExtraFreeze) flag = false;
        }
        if (flag)
        {
          foreach (Part part in __instance.parts)
          {
            PDamMod pdm = part.GetOverride2().GetValueOrDefault();
            if (pdm == ModEntry.Instance.frozen)
            {
              part.SetOverride2(new PDamMod?());
            }
          }
        }
      }
      if (__instance.Get(ModEntry.Instance.Taunted_Status.Status) > 0)
      {
        c.Queue(new AStatus()
        {
          targetPlayer = __instance != c.otherShip,
          status = ModEntry.Instance.Taunted_Status.Status,
          statusAmount = -1
        });
      }
    }
    private static void OnBeginTurn_Postfix(Ship __instance, State s)
    {
      if (__instance == s.ship)
      {
        foreach (Part part in __instance.parts)
        {
          PDamMod? pdm = part.GetOverride2().GetValueOrDefault();
          if (pdm == ModEntry.Instance.frozen)
          {
            part.SetOverride2(new PDamMod?());
          }
        }
      }
    }
  }
}