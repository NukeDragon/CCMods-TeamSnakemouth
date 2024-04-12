using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  public sealed class ApiImplementation : ISnakemouthApi
  {
    public IDeckEntry Vi_Deck => ModEntry.Instance.Vi_Deck;
    public IDeckEntry Kabbu_Deck => ModEntry.Instance.Kabbu_Deck;
    public IDeckEntry Leif_Deck => ModEntry.Instance.Leif_Deck;
    public IStatusEntry Charge_Status => ModEntry.Instance.Charge_Status;
    public IStatusEntry Concentration_Status => ModEntry.Instance.Concentration_Status;
    public IStatusEntry Frost_Status => ModEntry.Instance.Frost_Status;
    public IStatusEntry Poison_Status => ModEntry.Instance.Poison_Status;
    public IStatusEntry Taunted_Status => ModEntry.Instance.Taunted_Status;
    public IStatusEntry TP_Status => ModEntry.Instance.TP_Status;

    public void RegisterHook(ISnakemouthHook hook, double priority) => ModEntry.Instance.HookManager.Register(hook, priority);
    public void UnregisterHook(ISnakemouthHook hook) => ModEntry.Instance.HookManager.Unregister(hook);
  }
}
