using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class Defrost : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("Defrost", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Leif_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Defrost", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      data.cost = 1;
      return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
      List<CardAction> actions = new();
      switch (upgrade)
      {
        case Upgrade.None:
          Guid id1 = new Guid();
          List<CardAction> cardActionList1 = new List<CardAction>()
                {
            ModEntry.Instance.KokoroApi.ActionCosts.Make(
                    cost: ModEntry.Instance.KokoroApi.ActionCosts.Cost(
                        resource: ModEntry.Instance.KokoroApi.ActionCosts.StatusResource(
                          status: ModEntry.Instance.TP_Status.Status,
                          target: IKokoroApi.IActionCostApi.StatusResourceTarget.Player,
                         costSatisfiedIcon: ModEntry.Instance.TPCost.Sprite,
                         costUnsatisfiedIcon: ModEntry.Instance.TPCostOff.Sprite
                        ),
                        amount: 2
                      ),
                      action: ModEntry.Instance.KokoroApi.Actions.MakeContinue(out id1)
                    ),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new AStatus()
            {
              targetPlayer = true,
              status = Status.evade,
              statusAmount = 1
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new AStatus()
            {
              targetPlayer = true,
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = -2
            })
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          Guid id2 = new Guid();
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
            ModEntry.Instance.KokoroApi.ActionCosts.Make(
                    cost: ModEntry.Instance.KokoroApi.ActionCosts.Cost(
                        resource: ModEntry.Instance.KokoroApi.ActionCosts.StatusResource(
                          status: ModEntry.Instance.TP_Status.Status,
                          target: IKokoroApi.IActionCostApi.StatusResourceTarget.Player,
                         costSatisfiedIcon: ModEntry.Instance.TPCost.Sprite,
                         costUnsatisfiedIcon: ModEntry.Instance.TPCostOff.Sprite
                        ),
                        amount: 2
                      ),
                      action: ModEntry.Instance.KokoroApi.Actions.MakeContinue(out id2)
                    ),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new AStatus()
            {
              targetPlayer = true,
              status = Status.evade,
              statusAmount = 2
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new AStatus()
            {
              targetPlayer = true,
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = -2
            })
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new AStatus()
            {
              targetPlayer = true,
              status = Status.evade,
              statusAmount = 1
            },
            new AStatus()
            {
              targetPlayer = true,
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = -1
            }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}