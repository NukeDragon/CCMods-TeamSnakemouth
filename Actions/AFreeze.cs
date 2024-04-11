using FMOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  public class AFreeze : CardAction
  {
    public int worldX;
    public bool targetPlayer;
    public bool quick = false;

    public override void Begin(G g, State s, Combat c)
    {
      if (quick) this.timer = 0;
      else this.timer *= 0.5;
      Part partAtWorldX = (this.targetPlayer ? s.ship : c.otherShip).GetPartAtWorldX(this.worldX)!;
      if (partAtWorldX != null)
      {
        partAtWorldX.SetOverride2(ModEntry.Instance.frozen);
      }
      else
        this.timer = 0.0;
    }
  }
}
