using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.CanMoveBehaviors
{
    public interface ICanMoveBehavior
    {
        bool CanMove(PlayableUnit unit, int x, int y);
    }
}
