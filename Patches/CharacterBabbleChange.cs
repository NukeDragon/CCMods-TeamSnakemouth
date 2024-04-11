using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using FSPRO;
using FMOD;
using Nickel;

namespace NukeDragon.TeamSnakemouth
{
    internal class CharacterBabbleChange
    {
        public static void ApplyPatches(Harmony harmony)
        {
            harmony.Patch(AccessTools.DeclaredMethod(typeof(Shout), nameof(Shout.GetCharBabble)), postfix: new HarmonyMethod(typeof(CharacterBabbleChange), nameof(GetCharBabble_Postfix)));
        }
        private static void GetCharBabble_Postfix(string name, ref GUID __result)
        {
            if (name == ModEntry.Instance.Vi_Deck.Deck.Key())
            {
                __result = Event.Babble_periBaby;
            }
        }
    }
}
