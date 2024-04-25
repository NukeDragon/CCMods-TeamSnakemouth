using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class SecretStash : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("SecretStash", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SecretStash", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      int num = 0;
      switch (upgrade)
      {
        case Upgrade.None:
          num = 1;
          break;
        case Upgrade.A:
          num = 2;
          break;
        case Upgrade.B:
          num = 1;
          break;
      }
      data.cost = num;
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
                    cost = 4,
                    action = new AHeal
                      {
                      targetPlayer = true,
                      healAmount = 1,
                      canRunAfterKill = true,
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
                    cost = 7,
                    action = new AHeal
                      {
                      targetPlayer = true,
                      healAmount = 2,
                      canRunAfterKill=true,
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
                    cost = 3,
                    action = new AHeal
                      {
                      targetPlayer = true,
                      healAmount = 1,
                      canRunAfterKill=true
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