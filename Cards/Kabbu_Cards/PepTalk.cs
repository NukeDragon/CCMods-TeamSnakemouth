using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class PepTalk : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("PepTalk", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Kabbu_Deck.Deck,
          rarity = Rarity.rare,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PepTalk", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 1;
      data.description = ModEntry.Instance.Localizations.Localize(["card", "PepTalk", "description", upgrade.ToString()]);
      data.exhaust = true;
      switch (upgrade)
      {
        case Upgrade.None:
          break;
        case Upgrade.A:
          data.cost = 0;
          data.buoyant = true;
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
          AInspire aInspire = new AInspire()
          {
            statusAmount = 2
          };
          List<CardAction> cardActionList1 = new List<CardAction>()
                {
             new ATPCostAction()
             {
               action = aInspire,
               cost = 7
             }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          AInspire aInspire2 = new AInspire()
          {
            statusAmount = 2
          };
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
             new ATPCostAction()
             {
               action = aInspire2,
               cost = 7
             }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          AInspire aInspire3 = new AInspire()
          {
            statusAmount = 3
          };
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
             new ATPCostAction()
             {
               action = aInspire3,
               cost = 7
             }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }
  }
}