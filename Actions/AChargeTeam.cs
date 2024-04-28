using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal class AChargeTeam : CardAction
  {
    public int statusAmount = 0;
    public override void Begin(G g, State s, Combat c)
    {
      List<Character> crew = s.characters;
      List<CardAction> actions = new List<CardAction>();
      foreach (Character character in crew)
      {
        Deck deck = character.deckType.GetValueOrDefault();
        IStatusEntry? status;
        if (!ModEntry.Instance.Charged_Status_Dictionary.TryGetValue(deck, out status)) return;
        if (status != null)
        {
          actions.Add(new AStatus
          {
            status = status.Status,
            statusAmount = statusAmount,
            targetPlayer = true,
          });
        }
      }
      c.QueueImmediate(actions);
    }
    public override List<Tooltip> GetTooltips(State s)
    {
      List<Tooltip> tooltips = [
        new GlossaryTooltip(key: $"{ModEntry.Instance.Package.Manifest.UniqueName}::ChargeTeamAction")
        {
          Icon = ModEntry.Instance.ChargeTeamActionSprite.Sprite,
          Title = ModEntry.Instance.Localizations.Localize(["tooltips", "actions", "chargeTeam", "name"], new
          {
            amount = statusAmount,
          }),
          Description = ModEntry.Instance.Localizations.Localize(["tooltips", "actions", "chargeTeam", "description"], new
          {
            amount = statusAmount
          }),
          TitleColor = Colors.action
        }
        ];
      return tooltips;
    }

    public override Icon? GetIcon(State s) => new(ModEntry.Instance.ChargeTeamActionSprite.Sprite, statusAmount, Colors.textMain);
  }
}
