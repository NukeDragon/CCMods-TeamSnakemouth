using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace NukeDragon.TeamSnakemouth.Cards
{
  internal sealed class IceRain : Card, ICard
  {
    public static void Register(IModHelper helper)
    {
      helper.Content.Cards.RegisterCard("IceRain", new()
      {
        CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
        Meta = new()
        {
          deck = ModEntry.Instance.Leif_Deck.Deck,
          rarity = Rarity.uncommon,
          upgradesTo = [Upgrade.A, Upgrade.B]
        },
        Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "IceRain", "name"]).Localize
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
          break;
        case Upgrade.A:
          data.cost = 1;
          break;
        case Upgrade.B:
          data.cost = 1;
          data.exhaust = true;
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
          Missile missile1 = new Missile()
          {
            missileType = ModEntry.Instance.ice,
            yAnimation = 0,
            targetPlayer = false,
          };
          List<CardAction> cardActionList1 = new List<CardAction>()
                {
             new ATPCostAction
             {
               cost = 4,
               action =  ModEntry.Instance.KokoroApi.Actions.MakeContinue(out id1)
             },
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new ASpawn()
            {
              thing = missile1,
              offset = -1
            }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new ASpawn()
            {
              thing = missile1
            }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new ASpawn()
            {
              thing = missile1,
              offset = 1
            }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id1, new AStatus()
            {
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 1,
              targetPlayer = true,
            })
                };
          actions = cardActionList1;
          break;
        case Upgrade.A:
          Guid id2 = new Guid();
          Missile missile2 = new Missile()
          {
            missileType = ModEntry.Instance.ice,
            yAnimation = 0,
            targetPlayer = false,
          };
          List<CardAction> cardActionList2 = new List<CardAction>()
          {
                new ATPCostAction
                {
                  cost = 4,
                  action = ModEntry.Instance.KokoroApi.Actions.MakeContinue(out id2)
                },
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new ASpawn()
            {
              thing = missile2,
              offset = -1
            }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new ASpawn()
            {
              thing = missile2
            }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new ASpawn()
            {
              thing = missile2,
              offset = 1
            }),
                    ModEntry.Instance.KokoroApi.Actions.MakeContinued(id2, new AStatus()
            {
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 1,
              targetPlayer = true,
            })
                };
          actions = cardActionList2;
          break;
        case Upgrade.B:
          Missile missile3 = new Missile()
          {
            missileType = ModEntry.Instance.ice,
            yAnimation = 0,
            targetPlayer = false,
          };
          List<CardAction> cardActionList3 = new List<CardAction>()
                {
                   new ASpawn()
                   {
                     thing = missile3,
                     offset = -1
                   },
                   new ASpawn()
                   {
                     thing = missile3
                   },
                   new ASpawn()
                   {
                     thing = missile3,
                     offset = 1
                   },
                   new AStatus()
                   {
              status = ModEntry.Instance.Frost_Status.Status,
              statusAmount = 1,
              targetPlayer = true,
                   }
                };
          actions = cardActionList3;
          break;
      }
      return actions;
    }

  }
}