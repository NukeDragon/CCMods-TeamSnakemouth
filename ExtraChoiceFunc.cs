using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal class ExtraChoiceFunc
  {
    public static List<Choice> KnightForceDuel(State s)
    {
      List<Choice> choiceList = new List<Choice>();
      Choice choice1 = new Choice();
      choice1.label = "Fine.";
      choice1.actions.Add((CardAction)new AKnightHonor()
      {
        acceptedADuel = true
      });
      choiceList.Add(choice1);
      return choiceList;
    }
  }
}
