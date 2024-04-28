using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class Rejuvenate : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("Rejuvenate", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Leif_Deck.Deck,
          rarity = Rarity.rare,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Rejuvenate", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.exhaust = true;
      data.cost = 1;
      switch (upgrade)
      {
        case Upgrade.None:
          break;
        case Upgrade.A:
          data.cost = 0;
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
            new AStatus
            {
              status = ModEntry.Instance.TP_Status.Status,
              statusAmount = 3,
              targetPlayer = true,
              canRunAfterKill = true
            },
            new AStatus
            {
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 3,
              targetPlayer = true
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new AStatus
            {
              status = ModEntry.Instance.TP_Status.Status,
              statusAmount = 3,
              targetPlayer = true,
              canRunAfterKill = true
            },
            new AStatus
            {
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 3,
              targetPlayer = true
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AStatus
            {
              status = ModEntry.Instance.TP_Status.Status,
              statusAmount = 3,
              targetPlayer = true,
              canRunAfterKill = true
            },
            new AStatus
            {
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 2,
              targetPlayer = true
            }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}