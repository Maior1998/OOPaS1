using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.CanAttackBehavior
{
    public interface ICanAttackBehavior
    {
        bool CanAttack(PlayableUnit source, PlayableUnit target);
    }
}
