using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class DashThrough : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("DashThrough", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Kabbu_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DashThrough", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 2;
      switch (upgrade)
      {
        case Upgrade.None:
          break;
        case Upgrade.A:
          data.flippable = true;
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
            new AMove
            {
              dir = 2,
              targetPlayer = true
            },
            new AAttack
            {
              damage = GetDmg(s, 2),
              piercing = true
            },
            new AMove
            {
              dir = 2,
              targetPlayer = true
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new AMove
            {
              dir = 2,
              targetPlayer = true
            },
            new AAttack
            {
              damage = GetDmg(s, 2),
              piercing = true
            },
            new AMove
            {
              dir = 2,
              targetPlayer = true
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AMove
            {
              dir = 2,
              targetPlayer = true
            },
            new AAttack
            {
              damage = GetDmg(s, 2),
              piercing = true
            },
            new AMove
            {
              dir = 2,
              targetPlayer = true
            },
            new AAttack
            {
              damage = GetDmg(s, 2),
              piercing = true
            }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}