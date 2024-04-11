using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class CrisbeeDonut : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("CrisbeeDonut", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.rare,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "CrisbeeDonut", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      switch (upgrade)
      {
        case Upgrade.None:
          data.cost = 1;
          data.singleUse = true;
          break;
        case Upgrade.A:
          data.cost = 1;
          data.singleUse = true;
          break;
        case Upgrade.B:
          data.cost = 2;
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
            new AStatus()
            {
              targetPlayer = true,
              status = ModEntry.Instance.TP_Status.Status,
              statusAmount = 6,
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new AStatus()
            {
              targetPlayer = true,
              status = ModEntry.Instance.TP_Status.Status,
              statusAmount = 10,
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AStatus()
            {
              targetPlayer = true,
              status = ModEntry.Instance.TP_Status.Status,
              statusAmount = 3,
            }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}