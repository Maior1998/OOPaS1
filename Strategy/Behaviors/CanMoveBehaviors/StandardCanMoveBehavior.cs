using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.CanMoveBehaviors
{
    class StandardCanMoveBehavior : ICanMoveBehavior
    {
        public bool CanMove(PlayableUnit unit, int x, int y)
        {
            return Math.Abs(unit.UnitCoordinates.X - x) <= unit.MaxMoveRange && Math.Abs(unit.UnitCoordinates.Y - y) <= unit.MaxMoveRange;
        }
    }
}
