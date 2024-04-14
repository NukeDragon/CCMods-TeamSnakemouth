using Nickel;
using System.Reflection;
using System.Collections.Generic;
using FMOD;
using System;
using static System.Collections.Specialized.BitVector32;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class RallyCry : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("RallyCry", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Kabbu_Deck.Deck,
          rarity = Rarity.rare,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "RallyCry", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      switch (upgrade)
      {
        case Upgrade.None:
          data.cost = 3;
          break;
        case Upgrade.A:
          data.cost = 2;
          break;
        case Upgrade.B:
          data.cost = 3;
          break;
      }
      data.description = ModEntry.Instance.Localizations.Localize(["card", "RallyCry", "description", upgrade.ToString()]);
      return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
      List<CardAction> actions = new();
      switch (upgrade)
      {
        case Upgrade.None:
          int num = s.ship.hullMax - s.ship.hull;
          List<CardAction> cardActionList1 = new List<CardAction>()
                {
            new ATPCostAction()
              {
              cost = 5,
              action = new AStatus
                {
              targetPlayer = true,
              status = ModEntry.Instance.Charge_Status.Status,
              statusAmount = num
                }
              }
            };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          int num2 = s.ship.hullMax - s.ship.hull;
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new ATPCostAction()
              {
              cost = 5,
              action = new AStatus
                {
              targetPlayer = true,
              status = ModEntry.Instance.Charge_Status.Status,
              statusAmount = num2
                }
              }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          int num3 = s.ship.hullMax - s.ship.hull;
          Guid id = new Guid();
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new ATPCostAction()
            {
              cost = 5,
              action = ModEntry.Instance.KokoroApi.Actions.MakeContinue(out id)
            },
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id, new AStatus()
            {
              targetPlayer = true,
              status = ModEntry.Instance.Charge_Status.Status,
              statusAmount = num3
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id, new AHeal()
            {
              targetPlayer = true,
              healAmount = 1
            }),
          };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}