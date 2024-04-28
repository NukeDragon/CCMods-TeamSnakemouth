using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class PoisonDart : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("PoisonDart", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B],
          dontOffer = true
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PoisonDart", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 0;
      data.temporary = true;
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
            new AStatus
            {
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 1,
              targetPlayer = false
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new AStatus
            {
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 2,
              targetPlayer = false
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AStatus
            {
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 1,
              targetPlayer = false
            },
            new ADrawCard
            {
              count = 1
            }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}