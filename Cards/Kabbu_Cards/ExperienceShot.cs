using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class ExperienceShot : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("Experience", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Kabbu_Deck.Deck,
          rarity = Rarity.rare,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ExperienceShot", "name"]).Localize
      });
    }
    public override CardData GetData(State state)
    {
      CardData data = new CardData();
      Upgrade upgrade = this.upgrade;
      data.cost = 2;
      data.exhaust = true;
      data.description = ModEntry.Instance.Localizations.Localize(["card", "ExperienceShot", "description", upgrade.ToString()], new
      {
        dmg = this.upgrade == Upgrade.A ? GetDmg(state, 3) : GetDmg(state, 2)
      });
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
      switch (upgrade)
      {
        case Upgrade.None:
          List<CardAction> cardActionList1 = new List<CardAction>();
          AAttack aattack1 = new AAttack()
          {
            damage = GetDmg(s, 2)
          };
          List<CardAction> onkillactions1 = new List<CardAction>()
          {
            new ATPMax()
            {
              amount = 2,
              canRunAfterKill = true,
            },
            new AStatus()
            {
              status = ModEntry.Instance.TP_Status.Status,
              statusAmount = 2,
              targetPlayer = true,
              canRunAfterKill = true
            }
          };
          aattack1.onKillActions = onkillactions1;
          cardActionList1.Add(aattack1);
          actions = cardActionList1;
          break;
        case Upgrade.A:
          List<CardAction> cardActionList2 = new List<CardAction>();
          AAttack aattack2 = new AAttack()
          {
            damage = GetDmg(s, 3),
            piercing = true
          };
          List<CardAction> onkillactions2 = new List<CardAction>()
          {
            new ATPMax()
            {
              amount = 2,
              canRunAfterKill = true,
            },
            new AStatus()
            {
              status = ModEntry.Instance.TP_Status.Status,
              statusAmount = 2,
              targetPlayer = true,
              canRunAfterKill = true
            }
          };
          aattack2.onKillActions = onkillactions2;
          cardActionList2.Add(aattack2);
          actions = cardActionList2;
          break;
        case Upgrade.B:
          List<CardAction> cardActionList3 = new List<CardAction>();
          AAttack aattack3 = new AAttack()
          {
            damage = GetDmg(s, 2)
          };
          List<CardAction> onkillactions3 = new List<CardAction>()
          {
            new ATPMax()
            {
              amount = 3,
              canRunAfterKill = true,
            },
            new AStatus()
            {
              status = ModEntry.Instance.TP_Status.Status,
              statusAmount = 3,
              targetPlayer = true,
              canRunAfterKill = true
            }
          };
          aattack3.onKillActions = onkillactions3;
          cardActionList3.Add(aattack3);
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}