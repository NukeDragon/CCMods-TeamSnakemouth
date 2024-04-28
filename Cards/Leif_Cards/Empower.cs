using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class Empower : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("Empower", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Leif_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Empower", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      data.cost = 1;
      Upgrade upgrade = this.upgrade;
      switch (upgrade)
      {
        case Upgrade.None:
          break;
        case Upgrade.A:
          break;
        case Upgrade.B:
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
            new ATPCostAction()
            {
              cost = 3,
              action = new AStatus
                      {
                      targetPlayer = true,
                      status = Status.overdrive,
                      statusAmount = 2
                      }
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new ATPCostAction()
            {
              cost = 3,
              action = new AStatus
                      {
                      targetPlayer = true,
                      status = Status.overdrive,
                      statusAmount = 3
                      }
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new ATPCostAction()
            {
              cost = 6,
              action =  new AStatus
                      {
                      targetPlayer = true,
                      status = Status.powerdrive,
                      statusAmount = 1
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