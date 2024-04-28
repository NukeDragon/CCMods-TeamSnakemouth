using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace NukeDragon.TeamSnakemouth
{
  internal class ArtifactBlacklist
  {
    public static void ApplyPatches(Harmony harmony)
    {
      harmony.Patch(AccessTools.DeclaredMethod(typeof(ArtifactReward), nameof(ArtifactReward.GetBlockedArtifacts)), postfix: new HarmonyMethod(typeof(ArtifactBlacklist), nameof(GetBlockedArtifacts_Postfix)));
    }
    private static void GetBlockedArtifacts_Postfix(State s, HashSet<Type> __result)
    {
      if (!Enumerable.Any<Artifact>((IEnumerable<Artifact>)s.EnumerateAllArtifacts(), (Func<Artifact, bool>)(a => a is ExplorersPermit)))
       __result.Add(typeof(QueensPermit));
    }
  }
}
