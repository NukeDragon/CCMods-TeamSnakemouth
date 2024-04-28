using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class SpoiledStash : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("SpoiledStash", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SpoiledStash", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 1;
      switch (upgrade)
      {
        case Upgrade.None:
          break;
        case Upgrade.A:
          data.cost = 0;
          break;
        case Upgrade.B:
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
            new AHeal()
            {
              targetPlayer = true,
              healAmount = 1,
              canRunAfterKill = true,
            },
            new AStatus()
            {
              targetPlayer = true,
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 2,
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new AHeal()
            {
              targetPlayer = true,
              healAmount = 1,
              canRunAfterKill = true,
            },
            new AStatus()
            {
              targetPlayer = true,
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 2,
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AHeal()
            {
              targetPlayer = true,
              healAmount = 2,
              canRunAfterKill = true,
            },
            new AStatus()
            {
              targetPlayer = true,
              status = ModEntry.Instance.Poison_Status.Status,
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