﻿using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class PoisonSting : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("PoisonSting", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PoisonSting", "name"]).Localize
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
          data.cost = 0;
          break;
        case Upgrade.B:
          data.cost = 1;
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
            new AAttack()
            {
              damage = this.GetDmg(s, 1),
              status = new Status?(ModEntry.Instance.Poison_Status.Status),
              statusAmount = 1
            }
          };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
          {
            new AAttack()
            {
              damage = this.GetDmg(s, 1),
              status = new Status?(ModEntry.Instance.Poison_Status.Status),
              statusAmount = 1
            }
          };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
          {
            new AAttack()
            {
              damage = this.GetDmg(s, 0),
              status = new Status?(ModEntry.Instance.Poison_Status.Status),
              statusAmount = 2
            }
          };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}