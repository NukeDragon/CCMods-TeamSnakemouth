using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class Icicle : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("Icicle", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Leif_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Icicle", "name"]).Localize
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
          data.cost = 2;
          break;
        case Upgrade.B:
          data.cost = 0;
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
          Missile missile1 = new Missile()
          {
            missileType = ModEntry.Instance.ice,
            yAnimation = 0,
            targetPlayer = false,
          };
          List<CardAction> cardActionList1 = new List<CardAction>()
                {
            new ASpawn()
            {
              thing = missile1
            },
            new AStatus()
            {
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 1,
              targetPlayer = true,
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          Missile missile2 = new Missile()
          {
            missileType = ModEntry.Instance.ice,
            yAnimation = 0,
            targetPlayer = false
          };
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new ASpawn()
            {
              thing = missile2
            },
            new ASpawn()
            {
              thing = missile2,
              offset = 1
            },
            new AStatus()
            {
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 1,
              targetPlayer = true,
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          Missile missile3 = new Missile()
          {
            missileType = ModEntry.Instance.ice,
            yAnimation = 0,
            targetPlayer = false,
          };
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new ASpawn()
            {
              thing = missile3
            },
             new AStatus()
            {
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 1,
              targetPlayer = true,
            }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}