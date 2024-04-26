using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth
{
  internal sealed class VenomBurst : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("VenomBurst", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "VenomBurst", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 1;
      switch (upgrade)
      {
        case Upgrade.None:
          break;
        case Upgrade.A:
          break;
        case Upgrade.B:
          data.cost = 3;
          break;
      }
      return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
      List<CardAction> actions = new();
      switch (upgrade)
      {
        case Upgrade.None:
          List<CardAction> cardActionList1 = new List<CardAction>()
                {
            new AVariableHint
            {
              status = ModEntry.Instance.Poison_Status.Status
            },
            new AAttack
            {
              damage = GetDmg(s, s.ship.Get(ModEntry.Instance.Poison_Status.Status) * 2),
              xHint = 2,
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new AVariableHint
            {
              status = ModEntry.Instance.Poison_Status.Status
            },
            new AAttack
            {
              damage = GetDmg(s, s.ship.Get(ModEntry.Instance.Poison_Status.Status) * 2),
              xHint = 2,
            },
            new AStatus
            {
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 0,
              mode = AStatusMode.Set,
              targetPlayer = true
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AVariableHint
            {
              status = ModEntry.Instance.Poison_Status.Status
            },
            new AStatus
            {
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = s.ship.Get(ModEntry.Instance.Poison_Status.Status),
              xHint = 1,
            },
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}