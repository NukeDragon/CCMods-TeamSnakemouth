using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class PrepareDarts : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("PrepareDarts", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PrepareDarts", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 1;
      data.description = ModEntry.Instance.Localizations.Localize(["card", "PrepareDarts", "description", upgrade.ToString()]);
      switch (upgrade)
      {
        case Upgrade.None:
          data.exhaust = true;
          break;
        case Upgrade.A:
          data.exhaust = true;
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
              cost = 3,
              action = new AAddCard
              {
                card = new PoisonDart(),
                destination = CardDestination.Deck,
                amount = 2
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
              cost = 3,
              action = new AAddCard
              {
                card = new PoisonDart(),
                destination = CardDestination.Deck,
                amount = 3
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
              cost = 1,
              action = new AAddCard
              {
                card = new PoisonDart(),
                destination = CardDestination.Hand,
                amount = 1
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