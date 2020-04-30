using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.AttackBehaviors
{
    public class RangeAttack : IAttackBehavior
    {
        public void Attack(PlayableUnit currUnit, PlayableUnit targetUnit)
        {
            int Dx = Math.Abs(currUnit.UnitCoordinates.X - targetUnit.UnitCoordinates.X);
            int Dy = Math.Abs(currUnit.UnitCoordinates.Y - targetUnit.UnitCoordinates.Y);
            targetUnit.Hp = Math.Max(0, targetUnit.Hp - (Dx <= 1 && Dy <= 1 ? currUnit.Damage / 2 : currUnit.Damage));
        }
    }
}
