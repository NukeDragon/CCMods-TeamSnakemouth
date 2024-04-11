using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal class ATPCostAction : CardAction
  {
    public int Cost;
    public CardAction? Action;

    public override void Begin(G g, State s, Combat c)
    {
      base.Begin(g, s, c);
      timer = 0;
      int currentTP = s.ship.Get(ModEntry.Instance.TP_Status.Status);
      if (currentTP < Cost || Action == null) return;
      s.ship.Add(ModEntry.Instance.TP_Status.Status, -Cost);
      c.QueueImmediate(Action);
    }
  }
}
