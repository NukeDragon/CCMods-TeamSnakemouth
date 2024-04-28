using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class BubbleShield : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("BubbleShield", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Leif_Deck.Deck,
          rarity = Rarity.rare,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BubbleShield", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 1;
      data.retain = true;
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
              cost = 7,
              action = kokoroApi.Actions.MakeContinue(out id1)
            },
            kokoroApi.Actions.MakeContinued(id1, new AStatus
            {
              targetPlayer = true,
              status = Status.perfectShield,
              statusAmount = 1,
            }),
            kokoroApi.Actions.MakeContinued(id1, new AStatus
            {
              targetPlayer = true,
              status = ModEntry.Instance.Leif_Char.MissingStatus.Status,
              statusAmount = 1,
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
              cost = 5,
              action = kokoroApi.Actions.MakeContinue(out id2)
            },
            kokoroApi.Actions.MakeContinued(id2, new AStatus
            {
              targetPlayer = true,
              status = Status.perfectShield,
              statusAmount = 1,
            }),
            kokoroApi.Actions.MakeContinued(id2, new AStatus
            {
              targetPlayer = true,
              status = ModEntry.Instance.Leif_Char.MissingStatus.Status,
              statusAmount = 1,
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
              cost = 9,
              action = kokoroApi.Actions.MakeContinue(out id3)
            },
            kokoroApi.Actions.MakeContinued(id3, new AStatus
            {
              targetPlayer = true,
              status = Status.perfectShield,
              statusAmount = 2,
            }),
            kokoroApi.Actions.MakeContinued(id3, new AStatus
            {
              targetPlayer = true,
              status = ModEntry.Instance.Leif_Char.MissingStatus.Status,
              statusAmount = 2,
            }),
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}