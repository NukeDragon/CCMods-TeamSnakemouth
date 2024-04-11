
using HarmonyLib;
using NukeDragon.TeamSnakemouth.Dialogue;

namespace NukeDragon.TeamSnakemouth;

internal static class MGPatches
{
  private static ModEntry Instance => ModEntry.Instance;

  internal static void ApplyPatches(Harmony harmony)
  {
    harmony.Patch(AccessTools.DeclaredMethod(typeof(MG), nameof(MG.DrawLoadingScreen)), prefix: new HarmonyMethod(typeof(MGPatches), nameof(DrawLoadingScreen_Prefix)), postfix: new HarmonyMethod(typeof(MGPatches), nameof(DrawLoadingScreen_Postfix)));
  }

  private static void DrawLoadingScreen_Prefix(MG __instance, ref int __state)
    => __state = __instance.loadingQueue?.Count ?? 0;

  private static void DrawLoadingScreen_Postfix(MG __instance, ref int __state)
  {
    if (__state <= 0)
      return;
    if ((__instance.loadingQueue?.Count ?? 0) > 0)
      return;
    EventDialogue.Inject();
    ArtifactDialogue.Inject();
    DB.eventChoiceFns["KnightForceDuel"] = typeof(ExtraChoiceFunc).GetMethod("KnightForceDuel", AccessTools.all)!;
  }
}