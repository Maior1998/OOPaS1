using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.MoveBehaviors
{
    public class StandardWalk : IMoveBehavior
    {
        public void MoveTo(PlayableUnit target, int x, int y)
        {
            target.UnitCoordinates.X = x;
            target.UnitCoordinates.Y = y;
        }
    }
}

