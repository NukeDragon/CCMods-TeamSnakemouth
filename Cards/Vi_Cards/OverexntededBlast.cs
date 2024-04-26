using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth
{
  internal sealed class OverextendedBlast : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("OverextendedBlast", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "OverextendedBlast", "name"]).Localize
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
            new AHurt
            {
              targetPlayer = true,
              hurtAmount = 1,
            },
            new AAttack
            {
              damage = GetDmg(s, 3)
            },
            new ATPCostAction
            {
              cost = 3,
              action = new AHeal
              {
                targetPlayer = true,
                healAmount = 1,
                canRunAfterKill = true,
              },
              canRunAfterKill = true
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new AHurt
            {
              targetPlayer = true,
              hurtAmount = 1,
            },
            new AAttack
            {
              damage = GetDmg(s, 3)
            },
            new ATPCostAction
            {
              cost = 3,
              action = new AHeal
              {
                targetPlayer = true,
                healAmount = 1,
                canRunAfterKill = true,
              },
              canRunAfterKill = true
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AHurt
            {
              targetPlayer = true,
              hurtAmount = 1,
            },
            new AAttack
            {
              damage = GetDmg(s, 3)
            },
            new ATPCostAction
            {
              cost = 6,
              action = new AHeal
              {
                targetPlayer = true,
                healAmount = 2,
                canRunAfterKill = true,
              },
              canRunAfterKill = true
            }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}