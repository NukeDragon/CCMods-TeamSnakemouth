using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class DeepTaunt : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("DeepTaunt", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Kabbu_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DeepTaunt", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      switch (upgrade)
      {
        case Upgrade.None:
          data.cost = 2;
          data.exhaust = true;
          break;
        case Upgrade.A:
          data.cost = 2;
          data.exhaust = true;
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
            new AAttack
            {
              damage = GetDmg(s, 2),
              status = ModEntry.Instance.Taunted_Status.Status,
              statusAmount = 2,
              stunEnemy = true,
            }
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>()
                {
             new AAttack
             {
              damage = GetDmg(s, 3),
              status = ModEntry.Instance.Taunted_Status.Status,
              statusAmount = 3,
              stunEnemy = true,
             }
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          Guid id1 = new Guid();
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
            new ATPCostAction
            {
              cost = 4,
              action = ModEntry.Instance.KokoroApi.Actions.MakeContinue(out id1)
            },
            ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new AAttack
            {
              damage = GetDmg(s, 2),
              status = ModEntry.Instance.Taunted_Status.Status,
              statusAmount = 2,
              stunEnemy = true
            })
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}