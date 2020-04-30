using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.StandWithOtherUnitBehaviors
{
    class NoStandWithOthers : ICanStandWithOtherUnitBehavior
    {
        public bool CanStandWithOtherUnit(PlayableUnit other)
        {
            return false;
        }
    }
}
