using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal class AFlashFreeze : CardAction
  {
    public bool targetPlayer;

    public override void Begin(G g, State s, Combat c)
    {
      this.timer = 1;
      Ship ship = this.targetPlayer ? s.ship : c.otherShip;
      if (ship == null)
        return;
      ship.Set(ModEntry.Instance.Frost_Status.Status, 0);
      if (targetPlayer)
      {
        int num = 0;
        foreach (Part part in ship.parts)
        {
          c.QueueImmediate(new AFreeze() { worldX = ship.x + num, targetPlayer = true, quick = true });
          num++;
        }
        c.Queue(new AEndTurn());
      }
      if (!targetPlayer)
      {
        int num = 0;
        foreach (Part part in ship.parts)
        {
          c.QueueImmediate(new AFreeze() { worldX = ship.x + num, targetPlayer = false, quick = true});
          num++;
        }
      }
    }
  }
}
