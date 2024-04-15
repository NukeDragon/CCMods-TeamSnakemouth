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

namespace NukeDragon.TeamSnakemouth
{
  internal static class PDamModOverrideExt
  {
    public static PDamMod? GetOverride2(this Part self) => ModEntry.Instance.Helper.ModData.GetOptionalModData<PDamMod>(self, "PDamModOverride2");
    public static void SetOverride2(this Part self, PDamMod? value) => ModEntry.Instance.Helper.ModData.SetOptionalModData<PDamMod>(self, "PDamModOverride2", value);
  }

  internal static class PDamModPatches
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(Part), nameof(Part.GetDamageModifier)), prefix: new HarmonyMethod(typeof(PDamModPatches), nameof(GetDamMod_Prefix)));
    }

    public static bool GetDamMod_Prefix(Part __instance, ref PDamMod __result)
    {
      if (__instance.GetOverride2().HasValue)
      {
        __result = __instance.GetOverride2().GetValueOrDefault();
        return false;
      }
      return true;
    }
  }
}
