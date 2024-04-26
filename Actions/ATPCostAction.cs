using Nickel;
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
      action.whoDidThis = this.whoDidThis;
      c.QueueImmediate(action);
    }

    public override List<Tooltip> GetTooltips(State s)
    {
      List<Tooltip> list = new List<Tooltip>();
      list.Add(new GlossaryTooltip(key: $"{ModEntry.Instance.Package.Manifest.UniqueName}::TPCost")
      {
        Icon = ModEntry.Instance.TPCost.Sprite,
        Title = ModEntry.Instance.Localizations.Localize(["tooltips", "actions","tpcost", "name"]),
        Description = ModEntry.Instance.Localizations.Localize(["tooltips", "actions", "tpcost", "description"], new
        {
          amount = cost
        }),
        TitleColor = Colors.action
      });
      if (action == null) return list;
      foreach (Tooltip tooltip in action.GetTooltips(s))
      {
        list.Add(tooltip);
      }
      return list;
    }
  }
}
