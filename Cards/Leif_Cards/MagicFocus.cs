using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class MagicFocus : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("MagicFocus", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Leif_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "MagicFocus", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      switch (upgrade)
      {
        case Upgrade.None:
          data.cost = 0;
          break;
        case Upgrade.A:
          data.cost = 0;
          break;
        case Upgrade.B:
          data.cost = 1;
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
              cost = 2,
              action = new AStatus()
                      {
                        targetPlayer = true,
                        status = ModEntry.Instance.Concentration_Status.Status,
                        statusAmount = 1
                      }
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 1,
              action = new AStatus()
                      {
                        targetPlayer = true,
                        status = ModEntry.Instance.Concentration_Status.Status,
                        statusAmount = 1
                      }
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 3,
              action = new AStatus()
                      {
                        targetPlayer = true,
                        status = ModEntry.Instance.Concentration_Status.Status,
                        statusAmount = 2
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