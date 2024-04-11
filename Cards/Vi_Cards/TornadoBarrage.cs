using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class TornadoBarrage : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("TornadoBarrage", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Vi_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "TornadoBarrage", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      int num = 0;
      switch (upgrade)
      {
        case Upgrade.None:
          num = 1; 
          break;
        case Upgrade.A:
          num = 1;
          break;
        case Upgrade.B:
          num = 1;
          break;
      }
      data.cost = num;
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
                        damage = this.GetDmg(s, 1),
                        whoDidThis = this.GetMeta().deck
                    }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new AAttack()
                    {
                        damage = this.GetDmg(s, 1),
                        whoDidThis = this.GetMeta().deck
                    }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new AAttack()
                    {
                        damage = this.GetDmg(s, 1),
                        whoDidThis = this.GetMeta().deck
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
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new AAttack()
                    {
                        damage = this.GetDmg(s, 1),
                        whoDidThis = this.GetMeta().deck
                    }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new AAttack()
                    {
                        damage = this.GetDmg(s, 1),
                        whoDidThis = this.GetMeta().deck
                    }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new AAttack()
                    {
                        damage = this.GetDmg(s, 1),
                        whoDidThis = this.GetMeta().deck
                    })
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
                        amount: 4
                      ),
                      action: ModEntry.Instance.KokoroApi.Actions.MakeContinue(out id3)
                    ),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id3, new AAttack()
                    {
                        damage = this.GetDmg(s, 1),
                        whoDidThis = this.GetMeta().deck
                    }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id3, new AAttack()
                    {
                        damage = this.GetDmg(s, 1),
                        whoDidThis = this.GetMeta().deck
                    }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id3, new AAttack()
                    {
                        damage = this.GetDmg(s, 1),
                        whoDidThis = this.GetMeta().deck
                    }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id3, new AAttack()
                    {
                        damage = this.GetDmg(s, 1),
                        whoDidThis = this.GetMeta().deck
                    })
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}