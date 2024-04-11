using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal class AInspire : CardAction
  {
    public int statusAmount = 0;

    public override Route? BeginWithRoute(G g, State state, Combat combat)
      => new CharacterChoiceRoute()
      {
        Chars = state.characters,
        InspirationStatusAmount = statusAmount,
        Mode = 1
      };
  }

  internal class AArtifactChooseCharacter : CardAction
  {
    public Artifact? artifact;

    public override Route? BeginWithRoute(G g, State state, Combat combat)
      => new CharacterChoiceRoute()
      {
        Chars = state.characters,
        ArtifactChoice = artifact,
        Mode = 2
      };
  }
}
