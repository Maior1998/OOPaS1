using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.AttackBehaviors
{
    public class MeleeAttack : IAttackBehavior
    {
        public void Attack(PlayableUnit currUnit, PlayableUnit targetUnit)
        {
            targetUnit.Hp = Math.Max(0, targetUnit.Hp - currUnit.Damage);
        }
    }
}
