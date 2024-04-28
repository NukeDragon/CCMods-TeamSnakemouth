using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth.Dialogue
{
  internal class CombatDialogue
  {
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
      string kabbu = Instance.Kabbu_Deck.UniqueName;
      string vi = Instance.Vi_Deck.UniqueName;
      string leif = Instance.Leif_Deck.UniqueName;

      DB.story.all[$"{kabbu}JustHit_0"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        whoDidThat = Instance.Kabbu_Deck.Deck,
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Take that!",
            loopTag = "angy"
          },
        new SaySwitch
        {
          lines = [
              new CustomSay
              {
                who = vi,
                Text = "Good one!",
                loopTag = "happy"
              },
          new CustomSay
          {
            who = leif,
                Text = "Get 'em, Kabbu.",
                loopTag = "neutral"
              },
            ]
          } 
        ]
      };
      DB.story.all[$"{kabbu}JustHit_1"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        whoDidThat = Instance.Kabbu_Deck.Deck,
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Hah!",
            loopTag = "angy"
          }
        ]
      };
      DB.story.all[$"{kabbu}JustHit_2"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        whoDidThat = Instance.Kabbu_Deck.Deck,
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Let that be a lesson to you!",
            loopTag = "angy"
          }
        ]
      };
      DB.story.all[$"{vi}JustHit_0"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        whoDidThat = Instance.Vi_Deck.Deck,
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "Heheh.",
            loopTag = "happy"
          },
          new SaySwitch
          {
            lines = [
              new CustomSay
              {
                who = kabbu,
                Text = "Nice work, Vi!",
                loopTag = "neutral"
              },
            ]
          }
        ]
      };
      DB.story.all[$"{vi}JustHit_1"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        whoDidThat = Instance.Vi_Deck.Deck,
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "Nice.",
            loopTag = "neutral"
          },
          new SaySwitch
          {
            lines = [
              new CustomSay
              {
                who = leif,
                Text = "Nice.",
                loopTag = "neutral"
              },
            ]
          }
        ]
      };
      DB.story.all[$"{vi}JustHit_2"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        whoDidThat = Instance.Vi_Deck.Deck,
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "This is easy.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"{leif}JustHit_0"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        whoDidThat = Instance.Leif_Deck.Deck,
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "Boom.",
            loopTag = "neutral"
          },
          new SaySwitch
          {
            lines = [
              new CustomSay
              {
                who = vi,
                Text = "Wow!",
                loopTag = "neutral"
              },
            ]
          }
        ]
      };
      DB.story.all[$"{leif}JustHit_1"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        whoDidThat = Instance.Leif_Deck.Deck,
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "Got 'em.",
            loopTag = "neutral"
          },
          new SaySwitch
          {
            lines = [
              new CustomSay
              {
                who = kabbu,
                Text = "Great shot, Leif!",
                loopTag = "neutral"
              },
            ]
          }
        ]
      };
      DB.story.all[$"{leif}JustHit_2"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        whoDidThat = Instance.Leif_Deck.Deck,
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "More where that came from.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"BlockedAnEnemyAttackWithArmor_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        enemyShotJustHit = true,
        minDamageBlockedByPlayerArmorThisTurn = 1,
        oncePerCombatTags = ["WowArmorISPrettyCoolHuh"],
        oncePerRun = true,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "It was a good idea to have armor there.",
            loopTag = "thinking"
          }
        ]
      };
      DB.story.all[$"BlockedAnEnemyAttackWithArmor_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        enemyShotJustHit = true,
        minDamageBlockedByPlayerArmorThisTurn = 1,
        oncePerCombatTags = ["WowArmorISPrettyCoolHuh"],
        oncePerRun = true,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "Wow! That armor is actually helpful!",
            loopTag = "happy"
          }
        ]
      };
      DB.story.all[$"BlockedAnEnemyAttackWithArmor_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        enemyShotJustHit = true,
        minDamageBlockedByPlayerArmorThisTurn = 1,
        oncePerCombatTags = ["WowArmorISPrettyCoolHuh"],
        oncePerRun = true,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "We would like to put more armor on the ship.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"EnemyArmorHit_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        playerShotJustHit = true,
        minDamageBlockedByEnemyArmorThisTurn = 1,
        oncePerCombat= true,
        oncePerRun = true,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Looks like we need to use piercing attacks, team!",
            loopTag = "explaining"
          }
        ]
      };
      DB.story.all[$"EnemyArmorHit_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        playerShotJustHit = true,
        minDamageBlockedByEnemyArmorThisTurn = 1,
        oncePerCombat = true,
        oncePerRun = true,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "I hate enemies who have high defense.",
            loopTag = "unamused"
          }
        ]
      };
      DB.story.all[$"EnemyArmorHit_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        playerShotJustHit = true,
        minDamageBlockedByEnemyArmorThisTurn = 1,
        oncePerCombat = true,
        oncePerRun = true,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "They are armored. What can we do about that?",
            loopTag = "thinking"
          }
        ]
      };
      DB.story.all[$"HandOnlyHasTrashCards_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        handFullOfTrash = true,
        oncePerCombatTags = ["handOnlyHasTrashCards"],
        oncePerRun = true,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Team, we need to clear out some of this trash.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"EnemyHasWeakness_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        enemyHasWeakPart = true,
        oncePerRunTags = ["yelledAboutWeakness"],
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Team! I see a weak point!",
            loopTag = "angy"
          }
        ]
      };
      DB.story.all[$"EnemyHasWeakness_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        enemyHasWeakPart = true,
        oncePerRunTags = ["yelledAboutWeakness"],
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "A spot where I can deal more damage? Awesome!",
            loopTag = "happy"
          }
        ]
      };
      DB.story.all[$"EnemyHasWeakness_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        enemyHasWeakPart = true,
        oncePerRunTags = ["yelledAboutWeakness"],
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "We should really line up with that weak spot.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"EnemyHasBrittle_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        enemyHasBrittlePart = true,
        oncePerRunTags = ["yelledAboutBrittle"],
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Team, lining up that brittle spot will be a major strategical advantage!",
            loopTag = "explaining"
          }
        ]
      };
      DB.story.all[$"EnemyHasBrittle_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        enemyHasBrittlePart = true,
        oncePerRunTags = ["yelledAboutBrittle"],
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "Double damage? Don't have to ask me twice!",
            loopTag = "happy"
          }
        ]
      };
      DB.story.all[$"EnemyHasBrittle_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        enemyHasBrittlePart = true,
        oncePerRunTags = ["yelledAboutBrittle"],
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "We see the possibility of big damage.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"HandOnlyHasTrashCards_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        handFullOfTrash = true,
        oncePerCombatTags = ["handOnlyHasTrashCards"],
        oncePerRun = true,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "Uh, how are we supposed to do anything like this?",
            loopTag = "sad"
          }
        ]
      };
      DB.story.all[$"HandOnlyHasTrashCards_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        handFullOfTrash = true,
        oncePerCombatTags = ["handOnlyHasTrashCards"],
        oncePerRun = true,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "How did this happen?",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"JustHitGeneric_{kabbu}_0"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Great work!",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"JustHitGeneric_{kabbu}_1"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Keep it up!",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"JustHitGeneric_{vi}_0"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "Nice shot!",
            loopTag = "happy"
          }
        ]
      };
      DB.story.all[$"JustHitGeneric_{vi}_1"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "Woah!",
            loopTag = "happy"
          }
        ]
      };
      DB.story.all[$"JustHitGeneric_{leif}_0"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "That was a good one.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"JustHitGeneric_{leif}_1"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        playerShotJustHit = true,
        minDamageDealtToEnemyThisAction = 1,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "Well done.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"ManyTurns_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        minTurnsThisCombat = 9,
        oncePerCombatTags = ["manyTurns"],
        oncePerRun = true,
        turnStart = true,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Keep fighting, team! We will win eventually!",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"ManyTurns_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        minTurnsThisCombat = 9,
        oncePerCombatTags = ["manyTurns"],
        oncePerRun = true,
        turnStart = true,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "At this rate, I'd rather be fighting seedlings.",
            loopTag = "unamused"
          }
        ]
      };
      DB.story.all[$"ManyTurns_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        minTurnsThisCombat = 9,
        oncePerCombatTags = ["manyTurns"],
        oncePerRun = true,
        turnStart = true,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "We feel like our fights usually go quicker than this.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"OverheatGeneric_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        goingToOverheat = true,
        oncePerCombatTags = ["OverheatGeneric"],
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "Team, we need to keep the fire out of this ship.",
            loopTag = "explaining"
          }
        ]
      };
      DB.story.all[$"OverheatGeneric_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        goingToOverheat = true,
        oncePerCombatTags = ["OverheatGeneric"],
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "You're going to burn my fluff right off.",
            loopTag = "unamused"
          }
        ]
      };
      DB.story.all[$"OverheatGeneric_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        goingToOverheat = true,
        oncePerCombatTags = ["OverheatGeneric"],
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "Just because I have the magic to freeze things doesn't mean we can let fire roam around our ship.",
            loopTag = "ready"
          }
        ]
      };
      DB.story.all[$"ThatsALotOfDamageToUs_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        enemyShotJustHit = true,
        minDamageDealtToPlayerThisTurn = 3,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "You'll pay for that!",
            loopTag = "angy"
          }
        ]
      };
      DB.story.all[$"ThatsALotOfDamageToUs_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        enemyShotJustHit = true,
        minDamageDealtToPlayerThisTurn = 3,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "Yikes!",
            loopTag = "shocked"
          }
        ]
      };
      DB.story.all[$"ThatsALotOfDamageToUs_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        enemyShotJustHit = true,
        minDamageDealtToPlayerThisTurn = 3,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "Well that hurt.",
            loopTag = "hurt"
          }
        ]
      };
      DB.story.all[$"WeMissedOopsie_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        playerShotJustMissed = true,
        oncePerCombat = true,
        doesNotHaveArtifacts = ["Recalibrator", "GrazerBeam"],
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "At least it was a good try.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"WeMissedOopsie_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        playerShotJustMissed = true,
        oncePerCombat = true,
        doesNotHaveArtifacts = ["Recalibrator", "GrazerBeam"],
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "Whoops!",
            loopTag = "sad"
          }
        ]
      };
      DB.story.all[$"WeMissedOopsie_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        playerShotJustMissed = true,
        oncePerCombat = true,
        doesNotHaveArtifacts = ["Recalibrator", "GrazerBeam"],
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "We missed.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"WeGotHurtButNotTooBad_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        enemyShotJustHit = true,
        minDamageDealtToPlayerThisTurn = 1,
        maxDamageDealtToPlayerThisTurn = 1,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "We're still holding strong, team!",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"WeGotHurtButNotTooBad_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        enemyShotJustHit = true,
        minDamageDealtToPlayerThisTurn = 1,
        maxDamageDealtToPlayerThisTurn = 1,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "I guess that wasn't too bad...",
            loopTag = "sad"
          }
        ]
      };
      DB.story.all[$"WeGotHurtButNotTooBad_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        enemyShotJustHit = true,
        minDamageDealtToPlayerThisTurn = 1,
        maxDamageDealtToPlayerThisTurn = 1,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "That's fine.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"WeDidOverThreeDamage_{kabbu}"] = new()
      {
        type = NodeType.combat,
        allPresent = [kabbu],
        playerShotJustHit = true,
        minDamageDealtToEnemyThisTurn = 4,
        lines = [
          new CustomSay
          {
            who = kabbu,
            Text = "That was strong! Keep it up, team!",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"WeDidOverThreeDamage_{vi}"] = new()
      {
        type = NodeType.combat,
        allPresent = [vi],
        playerShotJustHit = true,
        minDamageDealtToEnemyThisTurn = 4,
        lines = [
          new CustomSay
          {
            who = vi,
            Text = "Niiice.",
            loopTag = "happy"
          }
        ]
      };
      DB.story.all[$"WeDidOverThreeDamage_{leif}"] = new()
      {
        type = NodeType.combat,
        allPresent = [leif],
        playerShotJustHit = true,
        minDamageDealtToEnemyThisTurn = 4,
        lines = [
          new CustomSay
          {
            who = leif,
            Text = "Keep the pressure up.",
            loopTag = "ready"
          }
        ]
      };
    }
  }
}
