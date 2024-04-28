using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class FrozenCoffin : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("FrozenCoffin", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Leif_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FrozenCoffin", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 2;
      switch (upgrade)
      {
        case Upgrade.None:
          break;
        case Upgrade.A:
          break;
        case Upgrade.B:
          data.exhaust = true;
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
            new ATPCostAction
            {
              cost = 5,
              action = new AStatus
                      {
                      targetPlayer = false,
                      status = ModEntry.Instance.Frost_Status.Status,
                      statusAmount = 3
                      }
            },
            new AStatus
            {
              targetPlayer = true,
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 3
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
          {
             new ATPCostAction
            {
              cost = 4,
              action = new AStatus
                      {
                      targetPlayer = false,
                      status = ModEntry.Instance.Frost_Status.Status,
                      statusAmount = 3
                      }
            },
            new AStatus
            {
              targetPlayer = true,
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 3
            }
          };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
          {
             new ATPCostAction
            {
              cost = 5,
              action = new AStatus
                      {
                      targetPlayer = false,
                      status = ModEntry.Instance.Frost_Status.Status,
                      statusAmount = 3
                      }
            }
          };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}