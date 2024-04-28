using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal class ATPMax : CardAction
  {
    public int amount;
    public override void Begin(G g, State s, Combat c)
    {
      foreach (Artifact artifact in s.EnumerateAllArtifacts())
      {
        if (artifact is ExplorersPermit)
        {
          ExplorersPermit explorersPermit = (ExplorersPermit)artifact;
          explorersPermit.countermax += amount;
        }
        else
        {
          if (artifact is QueensPermit)
          {
            QueensPermit queensPermit = (QueensPermit)artifact;
            queensPermit.countermax += amount;
          }
        }
      }
    }
  }
}
