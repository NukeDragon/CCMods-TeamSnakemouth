using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class Barricade : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("Barricade", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Kabbu_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Barricade", "name"]).Localize
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
      IKokoroApi kokoroApi = ModEntry.Instance.KokoroApi;
      switch (upgrade)
      {
        case Upgrade.None:
          Guid id1 = new Guid();
          List<CardAction> cardActionList1 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 2,
              action = kokoroApi.Actions.MakeContinue(out id1)
            },
            kokoroApi.Actions.MakeContinued(id1, new AStatus
            {
              status = Status.shield,
              statusAmount = 2,
              targetPlayer = true
            }),
            kokoroApi.Actions.MakeContinued(id1, new AStatus
            {
              status = Status.tempShield,
              statusAmount = 2,
              targetPlayer = true
            }),
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          Guid id2 = new Guid();
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 2,
              action = kokoroApi.Actions.MakeContinue(out id2)
            },
            kokoroApi.Actions.MakeContinued(id2, new AStatus
            {
              status = Status.shield,
              statusAmount = 2,
              targetPlayer = true
            }),
            kokoroApi.Actions.MakeContinued(id2, new AStatus
            {
              status = Status.tempShield,
              statusAmount = 2,
              targetPlayer = true
            }),
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
            Guid id3 = new Guid();
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 2,
              action = kokoroApi.Actions.MakeContinue(out id3)
            },
            kokoroApi.Actions.MakeContinued(id3, new AStatus
            {
              status = Status.shield,
              statusAmount = 3,
              targetPlayer = true
            }),
            kokoroApi.Actions.MakeContinued(id3, new AStatus
            {
              status = Status.tempShield,
              statusAmount = 3,
              targetPlayer = true
            }),
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}