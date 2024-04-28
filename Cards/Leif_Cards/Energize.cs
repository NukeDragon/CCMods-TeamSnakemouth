using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards

{
  internal sealed class Energize : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("Energize", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Leif_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Energize", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 0;
      switch (upgrade)
      {
        case Upgrade.None:
          break;
        case Upgrade.A:
          data.retain = true;
          break;
        case Upgrade.B:
          data.infinite = true;
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
              action = new AEnergy
              {
                changeAmount = 1
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
              cost = 2,
              action = new AEnergy
              {
                changeAmount = 1
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
              cost = 2,
              action = new AEnergy
              {
                changeAmount = 1
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