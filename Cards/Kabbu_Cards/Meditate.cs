using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class Meditate : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("Meditate", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Kabbu_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Meditate", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      int num = 2;
      bool b = false;
      if (upgrade == Upgrade.B)
      {
        num = 1;
        b = true;
      }
      data.cost = num;
      data.infinite = b; 
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
                      statusAmount = 2,
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
                      statusAmount = 3,
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
                      statusAmount = 1,
                    }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}