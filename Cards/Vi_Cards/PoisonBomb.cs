using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class PoisonBomb : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("PoisonBomb", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PoisonBomb", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.exhaust = true;
      switch (upgrade)
      {
        case Upgrade.None:
          data.cost = 1;
          break;
        case Upgrade.A:
          data.cost = 1;
          break;
        case Upgrade.B:
          data.cost = 0;
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
            new AStatus()
            {
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 3,
              targetPlayer = false
            },
            new AStatus()
            {
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 1,
              targetPlayer = true
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new AStatus()
            {
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 3,
              targetPlayer = false
            },
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AStatus()
            {
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 3,
              targetPlayer = false
            },
            new AStatus()
            {
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 1,
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