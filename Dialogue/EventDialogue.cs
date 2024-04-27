using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal class EventDialogue
  {
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
      string kabbu = Instance.Kabbu_Deck.UniqueName;
      string vi = Instance.Vi_Deck.UniqueName;
      string leif = Instance.Leif_Deck.UniqueName;

      DB.story.GetNode("GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "How about a rest?",
        loopTag = "sad",
      });
      DB.story.GetNode("GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "Free donuts!",
        loopTag = "neutral",
      });
      DB.story.GetNode("GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "I will take one of everything, please.",
        loopTag = "neutral",
      });

      DB.story.all[$"Knight_Midcombat_Greeting_Infinite_Kabbu_Multi_0"] = new()
      {
        type = NodeType.@event,
        oncePerCombat = true,
        allPresent = [kabbu],
        lookup = ["knight_duel"],
        requiredScenes = ["Knight_Midcombat_Greeting_1"],
        choiceFunc = "KnightForceDuel",
        lines = [
          new CustomSay() {
            flipped = true,
            who = "knight",
            Text = "I challenge ye to anothere duele! Do ye accepte?"
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "A duel? Team, this is a display of utmost honor! Surely we must accept!",
            loopTag = "neutral"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Wait maybe we should thin-",
            loopTag = "grumpy"
          },
          new CustomSay()
          {
            flipped = true,
            who = "knight",
            Text = "Excellente! En Garde!"
          }
        ]
      };
      /*
      DB.story.all[$"Vi_Intro"] = new() {
        once = true,
        type = NodeType.@event,
        lookup = ["zone_first"],
        allPresent = [vi],
        bg = "BGRunStart",
        lines = [
          new CustomSay()
          {
            who = "comp",
            Text = "Riiiiing Riiiiing! Time to get up.",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = vi,
            Text = "Ugh, where am I...",
            loopTag = "squint"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Okay, why do so many random people keep ending up in our ship?",
            loopTag = "squint",
            flipped = true
          },
          new CustomSay()
          {
            who = vi,
            Text = "Wait, have I been kidnapped?!",
            loopTag = "shocked"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "No, you have not been kidnapped.",
            loopTag = "grumpy",
            flipped = true
          },
          new CustomSay()
          {
            who = vi,
            Text = "And why should I trust you?",
            loopTag = "unamused"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Because if you don't I have a large incentive to cut the oxygen supply to restart this time loop.",
            loopTag = "grumpy",
            flipped = true
          },
          new CustomSay()
          {
            who = vi,
            Text = "Huh? Time loop? What does that mean?",
            loopTag = "sad"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Anyways, if we get into combat, will you be helpful?",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = vi,
            Text = "Yes, I am very good at dealing out smackdowns to all who oppose me.",
            loopTag = "happy"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Really? You look kind of short and weak and-",
            loopTag = "squint",
            flipped = true
          },
          new CustomSay()
          {
            who = vi,
            Text = "Shut up!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!",
            loopTag = "angy"
          }
        ]
      };
      DB.story.all[$"Leif_Intro"] = new()
      {
        once = true,
        type = NodeType.@event,
        lookup = ["zone_first"],
        allPresent = [leif],
        bg = "BGRunStart",
        lines = [
          new CustomSay()
          {
            who = "comp",
            Text = "Wakey Wakey!",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = leif,
            Text = "Vdf hmusa cgnadk afarvgnj!",
            loopTag = "hurt"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Whoa! Are you okay?",
            loopTag = "worried",
            flipped = true
          },
          new CustomSay()
          {
            who = leif,
            Text = "Yes yes, we're fine.",
            loopTag = "squint"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "What was that about??",
            loopTag = "worried",
            flipped = true
          },
          new CustomSay()
          {
            who = leif,
            Text = "Sorry, we're a bit sensitive to magic. We sense a lot of it around here.",
            loopTag = "thinking"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "That checks out. We have been stockpiling a large amount of magic blue crystals, for some reason.",
            loopTag = "neutral",
            flipped = true

          },
          new CustomSay()
          {
            who = leif,
            Text = "Magic blue crystals, you say...",
            loopTag = "neutral"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "So are you like a wizard, or something?",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = leif,
            Text = "We feel like the term 'sorcerer' fits us better, but yes.",
            loopTag = "neutral"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Then hopefully you should be able to help us with this ship aiming it's cannons at us, right?",
            loopTag = "squint",
            flipped = true
          },
          new CustomSay()
          {
            who = leif,
            Text = "Yes, we should be able to help with that.",
            loopTag = "ready"
          }
        ]
      };
      DB.story.all[$"Kabbu_Intro"] = new()
      {
        once = true,
        type = NodeType.@event,
        lookup = ["zone_first"],
        allPresent = [leif],
        bg = "BGRunStart",
        lines = [
          new CustomSay()
          {
            who = "comp",
            Text = "It's time to get up, you bedbugs!",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "Ow, my back...",
            loopTag = "squint"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Wow! An actual bedbug!",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "What is this? Where am I?",
            loopTag = "angy"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "You're in space! Now my turn: Who are you?",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "My name is Kabbu. I am an explorer, along with my team.",
            loopTag = "thinking",
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Your team?",
            loopTag = "squint",
            flipped = true
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "Team Snakemouth. A bee, a moth, and I. You wouldn't happened to have seen them, have you?",
            loopTag = "explaining"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "I have not, but they will probably turn up eventually. Anyways, are you good at fighting? We tend to do a lot of that here.",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "Do we have to? Surely there is a better way.",
            loopTag = "sad"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Yes.",
            loopTag = "neutral",
            flipped = true
          }
        ]
      };
      */
      DB.story.all[$"ChoiceCardRewardOfYourColorChoice_{kabbu}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [kabbu],
        bg = "BGBootSequence",
        lines = [
        new CustomSay()
        {
          who = kabbu,
          Text = "What creature is peeking inside my head!?",
          loopTag = "angy"
        },
          new CustomSay()
          {
            who = "comp",
            Text = "Energy readings are back to normal."
          }
      ]
      };
      DB.story.all[$"ChoiceCardRewardOfYourColorChoice_{vi}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [vi],
        bg = "BGBootSequence",
        lines = [
        new CustomSay()
        {
          who = vi,
          Text = "Nice! I learned a new skill!",
          loopTag = "happy",
        },
          new CustomSay()
          {
            who = "comp",
            Text = "Energy readings are back to normal."
          }
      ]
      };
      DB.story.all[$"ChoiceCardRewardOfYourColorChoice_{leif}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [leif],
        bg = "BGBootSequence",
        lines = [
          new CustomSay()
          {
            who = leif,
            Text = "We had never considered doing that. Thanks.",
            loopTag = "neutral"
          },
          new CustomSay()
        {
          who = "void",
          Text = "You are harder to teach than the rest.",
          loopTag = "neutral",
          flipped = true
        },
          new CustomSay()
          {
            who = leif,
            Text = "Should we take that as a compliment or an insult?",
            loopTag = "neutral"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "Energy readings are back to normal."
          }
      ]
      };
      DB.story.GetNode("AbandonedShipyard")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "Team, I don't know if we should trust this...",
        loopTag = "sad"
      });
      DB.story.GetNode("AbandonedShipyard_Repaired")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "Good thinking, team!",
        loopTag = "neutral"
      });
      DB.story.GetNode("AbandonedShipyard")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "That has to mean there is some big monster out here!",
        loopTag = "neutral"
      });
      DB.story.GetNode("AbandonedShipyard_Repaired")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "Aw, I was ready for a fight.",
        loopTag = "unamused"
      });
      DB.story.GetNode("AbandonedShipyard")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "Strange.",
        loopTag = "neutral"
      });
      DB.story.GetNode("AbandonedShipyard_Repaired")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "How anticlimactic. We were expecting something to happen.",
        loopTag = "neutral"
      });
      DB.story.GetNode("CrystallizedFriendEvent")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "Whatever is best for the team.",
        loopTag = "explaining"
      });
      DB.story.all[$"CrystallizedFriendEvent_{kabbu}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [kabbu],
        bg = "BGCrystalizedFriend",
        lines = [
          new Wait()
          {
            secs = 1.5
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "Let's go, team!",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"CrystallizedFriendEvent_{ModEntry.Instance.Kabbu_Deck.Deck}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [kabbu],
        bg = "BGCrystalizedFriend",
        lines = [
          new Wait()
          {
            secs = 1.5
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "Let's go, team!",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.GetNode("CrystallizedFriendEvent")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "But I'm an essential part of this team!",
        loopTag = "sad"
      });
      DB.story.all[$"CrystallizedFriendEvent_{vi}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [vi],
        bg = "BGCrystalizedFriend",
        lines = [
          new Wait()
          {
            secs = 1.5
          },
          new CustomSay()
          {
            who = vi,
            Text = "Nice!",
            loopTag = "happy"
          }
        ]
      };
      DB.story.all[$"CrystallizedFriendEvent_{ModEntry.Instance.Vi_Deck.Deck}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [vi],
        bg = "BGCrystalizedFriend",
        lines = [
          new Wait()
          {
            secs = 1.5
          },
          new CustomSay()
          {
            who = vi,
            Text = "Nice!",
            loopTag = "happy"
          }
        ]
      };
      DB.story.GetNode("CrystallizedFriendEvent")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "We do like the cryosleep.",
        loopTag = "thinking"
      });
      DB.story.all[$"CrystallizedFriendEvent_{leif}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [leif],
        bg = "BGCrystalizedFriend",
        lines = [
          new Wait()
          {
            secs = 1.5
          },
          new CustomSay()
          {
            who = leif,
            Text = "Ah. Hello.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"CrystallizedFriendEvent_{ModEntry.Instance.Leif_Deck.Deck}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [leif],
        bg = "BGCrystalizedFriend",
        lines = [
          new Wait()
          {
            secs = 1.5
          },
          new CustomSay()
          {
            who = leif,
            Text = "Ah. Hello.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.all[$"ShopkeeperInfinite_{kabbu}_Multi_0"] = new()
      {
        type = NodeType.@event,
        lookup = ["shopBefore"],
        allPresent = [kabbu],
        bg = "BGShop",
        lines = [
          new CustomSay()
          {
            who = "nerd",
            Text = "Welcome!",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "Greetings!",
            loopTag = "explaining",
          },
          new Jump()
          {
            key = "NewShop"
          }
        ]
      };
      DB.story.all[$"ShopkeeperInfinite_{kabbu}_Multi_1"] = new()
      {
        type = NodeType.@event,
        lookup = ["shopBefore"],
        allPresent = [kabbu],
        bg = "BGShop",
        lines = [
          new CustomSay()
          {
            who = "nerd",
            Text = "Heya!",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "What a cozy little shop!",
            loopTag = "thinking",
          },
          new Jump()
          {
            key = "NewShop"
          }
        ]
      };
      DB.story.all[$"ShopkeeperInfinite_{vi}_Multi_0"] = new()
      {
        type = NodeType.@event,
        lookup = ["shopBefore"],
        allPresent = [vi],
        bg = "BGShop",
        lines = [
          new CustomSay()
          {
            who = "nerd",
            Text = "Hello!",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = vi,
            Text = "Hi!",
            loopTag = "neutral",
          },
          new Jump()
          {
            key = "NewShop"
          }
        ]
      };
      DB.story.all[$"ShopkeeperInfinite_{vi}_Multi_1"] = new()
      {
        type = NodeType.@event,
        lookup = ["shopBefore"],
        allPresent = [vi],
        bg = "BGShop",
        lines = [
          new CustomSay()
          {
            who = "nerd",
            Text = "Howdy!",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = vi,
            Text = "What are your prices?",
            loopTag = "unamused",
          },
          new CustomSay()
          {
            who = "nerd",
            Text = "Uh? Free!",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = vi,
            Text = "Wow!",
            loopTag = "happy",
          },
          new Jump()
          {
            key = "NewShop"
          }
        ]
      };
      DB.story.all[$"ShopkeeperInfinite_{leif}_Multi_0"] = new()
      {
        type = NodeType.@event,
        lookup = ["shopBefore"],
        allPresent = [leif],
        bg = "BGShop",
        lines = [
          new CustomSay()
          {
            who = leif,
            Text = "What's up?",
            loopTag = "neutral"
          },
          new CustomSay()
          {
            who = "nerd",
            Text = "There is no up, we're in space!",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = leif,
            Text = "Then how are you sitting down?",
            loopTag = "neutral"
          },
          new Jump()
          {
            key = "NewShop"
          }
        ]
      };
      DB.story.all[$"ShopkeeperInfinite_{leif}_Multi_1"] = new()
      {
        type = NodeType.@event,
        lookup = ["shopBefore"],
        allPresent = [leif],
        bg = "BGShop",
        lines = [
          new CustomSay()
          {
            who = "nerd",
            Text = "Greetings!",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = leif,
            Text = "Right back at you.",
            loopTag = "neutral"
          },
          new Jump()
          {
            key = "NewShop"
          }
        ]
      };
      DB.story.GetNode("DraculaTime")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "Have we met before?",
        loopTag = "neutral"
      });
      DB.story.GetNode("DraculaTime")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "You seem like a weirdo.",
        loopTag = "squint"
      });
      DB.story.GetNode("DraculaTime")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "Should we recognize you?",
        loopTag = "thinking"
      });
      DB.story.GetNode("LoseCharacterCard")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "Team! Do not look down!!!",
        loopTag = "panic"
      });
      DB.story.GetNode("LoseCharacterCard")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "A what now?",
        loopTag = "sad"
      });
      DB.story.GetNode("LoseCharacterCard")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "We really think that trying to escape would be a good idea.",
        loopTag = "hurt"
      });
      DB.story.GetNode("LoseCharacterCard_No")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "I thought that was the end for us...",
        loopTag = "sad"
      });
      DB.story.GetNode("LoseCharacterCard_No")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "A little confused, did I miss something?",
        loopTag = "sad"
      });
      DB.story.GetNode("LoseCharacterCard_No")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "We would like to have a word with the pilot of this ship.",
        loopTag = "squint"
      });
      DB.story.GetNode("Sasha_2_Multi_2")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "You know, I could fancy a game of sports.",
        loopTag = "thinking"
      });
      DB.story.GetNode("Sasha_2_Multi_2")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "That means there are prizes, right?",
        loopTag = "happy"
      });
      DB.story.GetNode("Sasha_2_Multi_2")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "Sports.",
        loopTag = "neutral"
      });
      DB.story.all[$"LoseCharacterCard_{kabbu}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [kabbu],
        bg = "BGSupernova",
        lines = [
          new CustomSay()
          {
            who = kabbu,
            Text = "That was horrifying!",
            loopTag = "angy"
          },
          new CustomSay()
          {
            who = kabbu,
            Text = "May I suggest we steer clear of those from now on?",
            loopTag = "explaining"
          }
        ]
      };
      DB.story.all[$"LoseCharacterCard_{vi}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [vi],
        bg = "BGSupernova",
        lines = [
          new CustomSay()
          {
            who = vi,
            Text = "What just happened?",
            loopTag = "sad"
          },
          new CustomSay()
          {
            who = "comp",
            Text = "If we hadn't escaped right then, this ship and all of us would have been compressed into that infinitely dense point.",
            loopTag = "neutral",
            flipped = true
          },
          new CustomSay()
          {
            who = vi,
            Text = "...",
            loopTag = "shocked"
          }
        ]
      };
      DB.story.all[$"LoseCharacterCard_{leif}"] = new()
      {
        type = NodeType.@event,
        oncePerRun = true,
        allPresent = [leif],
        bg = "BGSupernova",
        lines = [
          new CustomSay()
          {
            who = leif,
            Text = "While we cannot deny that we were terrified, we also would be lying if we said we weren't fascinated.",
            loopTag = "neutral"
          }
        ]
      };
      DB.story.GetNode("Soggins_Infinite")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "Team, we should help out!",
        loopTag = "explaining"
      });
      DB.story.GetNode("Soggins_Infinite")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "You better have a reward ready, or I am just gonna blow you up anyways.",
        loopTag = "unamused"
      });
      DB.story.GetNode("Soggins_Infinite")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "How do you mess it up that badly?",
        loopTag = "neutral"
      });
      DB.story.GetNode("SogginsEscape_1")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = kabbu,
        Text = "Well I suppose that's fine... but not even a thank you?",
        loopTag = "sad"
      });
      DB.story.GetNode("SogginsEscape_1")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = vi,
        Text = "WOOOW. I HOPE YOU ARE PREPARED FOR A REAL SMACKDOWN BECAUSE YOU-",
        loopTag = "angy"
      });
      DB.story.GetNode("SogginsEscape_1")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
      {
        who = leif,
        Text = "Let's just blow him up.",
        loopTag = "ready"
      });
    }
  }
}
