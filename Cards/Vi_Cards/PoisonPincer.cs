using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class PoisonPincer : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("PoisonPincer", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PoisonPincer", "name"]).Localize
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
          data.flippable = true;
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
                        amount: 3
                      ),
                      action: ModEntry.Instance.KokoroApi.Actions.MakeContinue(out id1)
                    ),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new AAttack()
            {
              damage = GetDmg(s, 1),
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 1
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new AMove()
            {
              targetPlayer = true,
              dir = 2
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new AAttack()
            {
              damage = GetDmg(s, 1),
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 1
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new AMove()
            {
              targetPlayer = true,
              dir = -2
            }),
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
                        amount: 3
                      ),
                      action: ModEntry.Instance.KokoroApi.Actions.MakeContinue(out id2)
                    ),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new AAttack()
            {
              damage = GetDmg(s, 1),
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 1
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new AMove()
            {
              targetPlayer = true,
              dir = 2
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new AAttack()
            {
              damage = GetDmg(s, 1),
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 1
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new AMove()
            {
              targetPlayer = true,
              dir = -2
            }),
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          Guid id3 = new Guid();
          List<CardAction> cardActionList3 = new List<CardAction>()
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
                      action: ModEntry.Instance.KokoroApi.Actions.MakeContinue(out id3)
                    ),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id3, new AAttack()
            {
              damage = GetDmg(s, 1),
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 1
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id3, new AMove()
            {
              targetPlayer = true,
              dir = 2
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id3, new AAttack()
            {
              damage = GetDmg(s, 1),
              status = ModEntry.Instance.Poison_Status.Status,
              statusAmount = 1
            }),
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id3, new AMove()
            {
              targetPlayer = true,
              dir = -2
            }),
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}