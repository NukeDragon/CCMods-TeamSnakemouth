using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class TeamPlan : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("TeamPlan", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Kabbu_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "TeamPlan", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 0;
      data.infinite = true;
      data.floppable = true;
      switch (upgrade)
      {
        case Upgrade.None:
          break;
        case Upgrade.A:
          data.retain = true;
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
            new ATPCostAction 
            {
              cost = 2,
              action = new ADrawCard{count = 2, disabled = flipped},
              disabled = flipped
            },
            new ADummyAction(),
            new AExhaust{CardId = this.uuid, disabled = !flipped},
            new ADrawCard{count = 1, disabled = !flipped}
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 2,
              action = new ADrawCard{count = 2, disabled = flipped},
              disabled = flipped
            },
            new ADummyAction(),
            new AExhaust{CardId = this.uuid, disabled = !flipped},
            new ADrawCard{count = 1, disabled = !flipped}
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 2,
              action = new ADrawCard{count = 3, disabled = flipped},
              disabled = flipped
            },
            new ADummyAction(),
            new AExhaust{CardId = this.uuid, disabled = !flipped},
            new ADrawCard{count = 1, disabled = !flipped}
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}