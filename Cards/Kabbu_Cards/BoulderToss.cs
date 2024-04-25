﻿using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class BoulderToss : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("BoulderToss", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Kabbu_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BoulderToss", "name"]).Localize
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
          break;
        case Upgrade.B:
          break;
      }
      return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
      IKokoroApi kokoroApi = ModEntry.Instance.KokoroApi;
      List<CardAction> actions = new();
      switch (upgrade)
      {
        case Upgrade.None:
          Guid id1;
          List<CardAction> cardActionList1 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 3,
              action = kokoroApi.Actions.MakeContinue(out id1)
            },
            kokoroApi.Actions.MakeContinued(id1, new AAttack
            {
              damage = GetDmg(s, 2)
            }),
            kokoroApi.Actions.MakeContinued(id1, new ASpawn
            {
              thing = new Asteroid(),
              offset = -1
            }),
            kokoroApi.Actions.MakeContinued(id1, new ASpawn
            {
              thing = new Asteroid()
            }),
            kokoroApi.Actions.MakeContinued(id1, new ASpawn
            {
              thing = new Asteroid(),
              offset = 1
            }),
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          Guid id2;
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 3,
              action = kokoroApi.Actions.MakeContinue(out id2)
            },
            kokoroApi.Actions.MakeContinued(id2, new AAttack
            {
              damage = GetDmg(s, 3)
            }),
            kokoroApi.Actions.MakeContinued(id2, new ASpawn
            {
              thing = new Asteroid {bubbleShield = true},
              offset = -1
            }),
            kokoroApi.Actions.MakeContinued(id2, new ASpawn
            {
              thing = new Asteroid {bubbleShield = true}
            }),
            kokoroApi.Actions.MakeContinued(id2, new ASpawn
            {
              thing = new Asteroid {bubbleShield = true},
              offset = 1
            }),
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          Guid id3;
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 3,
              action = kokoroApi.Actions.MakeContinue(out id3)
            },
            kokoroApi.Actions.MakeContinued(id3, new ASpawn
            {
              thing = new SpaceMine(),
              offset = -1
            }),
            kokoroApi.Actions.MakeContinued(id3, new ASpawn
            {
              thing = new SpaceMine()
            }),
            kokoroApi.Actions.MakeContinued(id3, new ASpawn
            {
              thing = new SpaceMine(),
              offset = 1
            }),
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}