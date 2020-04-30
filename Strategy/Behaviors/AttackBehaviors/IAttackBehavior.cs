using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.AttackBehaviors
{
    public interface IAttackBehavior
    {
        void Attack(PlayableUnit currUnit, PlayableUnit targetUnit);
    }
}
