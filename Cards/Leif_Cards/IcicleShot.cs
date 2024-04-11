using Nickel;
using System.Reflection;
using System.Collections.Generic;
using NukeDragon.TeamSnakemouth.Patches;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class IcicleShot : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("IcicleShot", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Leif_Deck.Deck,
          rarity = Rarity.common,
          upgradesTo = [Upgrade.A, Upgrade.B],
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "IcicleShot", "name"]).Localize
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
          List<CardAction> cardActionList1 = new List<CardAction>();
          AAttack attack1 = new AAttack()
          {
            damage = this.GetDmg(s, 0),
          };
          AStatus status1 = new AStatus()
          {
            targetPlayer = true,
            status = ModEntry.Instance.Frost_Status.Status,
            statusAmount = 1
          };
          attack1.SetFreezing(true);
          cardActionList1.Add(attack1);
          cardActionList1.Add(status1);
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>();
          AAttack attack2 = new AAttack()
          {
            damage = this.GetDmg(s, 0),
          };
          attack2.SetFreezing(true);
          cardActionList2.Add(attack2);
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>();
          AAttack attack3 = new AAttack()
          {
            damage = this.GetDmg(s, 1),
          };
          AStatus status3 = new AStatus()
          {
            targetPlayer = true,
            status = ModEntry.Instance.Frost_Status.Status,
            statusAmount = 1
          };
          attack3.SetFreezing(true);
          cardActionList3.Add(attack3);
          cardActionList3.Add(status3);
          actions = cardActionList3;
          break;
      }
      return actions;
    }
  }
}