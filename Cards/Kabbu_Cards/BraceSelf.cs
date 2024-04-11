using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class BraceSelf : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("BraceSelf", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Kabbu_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BraceSelf", "name"]).Localize
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
          break;
        case Upgrade.A:
          data.cost = 0;
          break;
        case Upgrade.B:
          data.cost = 1;
          data.retain = true;
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
              status = Status.tempShield,
              statusAmount = 3
            },
            new AEndTurn() {}
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new AStatus()
            {
              targetPlayer = true,
              status = Status.shield,
              statusAmount = 1
            },
            new AStatus()
            {
              targetPlayer = true,
              status = Status.tempShield,
              statusAmount = 2
            },
            new AEndTurn() {}
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AStatus()
            {
              targetPlayer = true,
              status = Status.tempShield,
              statusAmount = 4
            },
            new AEndTurn() {}
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}