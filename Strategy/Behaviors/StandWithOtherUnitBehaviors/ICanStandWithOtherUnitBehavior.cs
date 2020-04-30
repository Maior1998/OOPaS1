using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.StandWithOtherUnitBehaviors
{
    public interface ICanStandWithOtherUnitBehavior
    {
        bool CanStandWithOtherUnit(PlayableUnit other);
    }
}
