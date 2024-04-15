using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  public class ATPCostAction : CardAction
  {
    public int cost;
    public CardAction? action;

    public override void Begin(G g, State s, Combat c)
    {
      base.Begin(g, s, c);
      timer = 0;
      int currentTP = s.ship.Get(ModEntry.Instance.TP_Status.Status);
      if (currentTP < cost || action == null) return;
      foreach (var hook in ModEntry.Instance.HookManager.GetHooksWithProxies(ModEntry.Instance.KokoroApi, s.EnumerateAllArtifacts())) hook.OnATPCost(this, s, c);
      s.ship.Add(ModEntry.Instance.TP_Status.Status, -cost);
      c.QueueImmediate(action);
    }
  }
}
