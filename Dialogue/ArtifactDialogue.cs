using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth.Dialogue
{
  internal class ArtifactDialogue
  {
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
      string kabbu = Instance.Kabbu_Deck.UniqueName;
      string vi = Instance.Vi_Deck.UniqueName;
      string leif = Instance.Leif_Deck.UniqueName;

      DB.story.GetNode("ArtifactGeminiCore_Multi_4")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "Hmm... They both look nice.",
        loopTag = "thinking",
      });
      DB.story.GetNode("ArtifactGeminiCore_Multi_4")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "I like the red.",
        loopTag = "neutral",
      });
      DB.story.GetNode("ArtifactGeminiCore_Multi_4")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "Blue. Obviously.",
        loopTag = "neutral",
      });
    }
  }
}
