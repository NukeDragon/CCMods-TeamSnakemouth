using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeDragon.TeamSnakemouth
{
  internal sealed class MouseDownHandler(Action @delegate) : OnMouseDown
  {
    private readonly Action Delegate = @delegate;

    public void OnMouseDown(G g, Box b)
      => Delegate();
  }
}
