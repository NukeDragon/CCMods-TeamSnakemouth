using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal class ATPCostAction : CardAction
  {
    public int cost;
    public CardAction? action;

    public override void Begin(G g, State s, Combat c)
    {
      base.Begin(g, s, c);
      timer = 0;
      int currentTP = s.ship.Get(ModEntry.Instance.TP_Status.Status);
      if (currentTP < cost || action == null) return;
      s.ship.Add(ModEntry.Instance.TP_Status.Status, -cost);
      c.QueueImmediate(action);
    }
  }
}
