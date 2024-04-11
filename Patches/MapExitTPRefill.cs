using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace NukeDragon.TeamSnakemouth
{
  internal class MapExitTPRefill
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(MapExit), nameof(MapExit.MakeRoute)), postfix: new HarmonyMethod(typeof(MapExitTPRefill), nameof(MakeRoute_Postfix)));
    }
    private static void MakeRoute_Postfix(State s, Route __result)
    {
      var artifact1 = s.EnumerateAllArtifacts().OfType<ExplorersPermit>().FirstOrDefault();
      if (artifact1 is not null)
      {
        artifact1.countercurrent = artifact1.countermax;
      }
      var artifact2 = s.EnumerateAllArtifacts().OfType<QueensPermit>().FirstOrDefault();
      if (artifact2 is not null)
      {
        artifact2.countercurrent = artifact2.countermax;
      }
    }
  }
}
