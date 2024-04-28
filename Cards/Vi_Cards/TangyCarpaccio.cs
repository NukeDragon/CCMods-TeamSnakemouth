﻿using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class TangyCarpaccio : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("TangyCarpaccio", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.rare,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "TangyCarpaccio", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 1;
      data.singleUse = true;
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
            new AHeal
            {
              healAmount = 3,
              canRunAfterKill = true,
              targetPlayer = true,
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new AHeal
            {
              healAmount = 3,
              canRunAfterKill = true,
              targetPlayer = true
            }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AHeal
            {
              healAmount = 4,
              canRunAfterKill = true,
              targetPlayer = true
            }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}