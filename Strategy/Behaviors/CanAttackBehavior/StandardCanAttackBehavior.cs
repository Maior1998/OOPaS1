using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.CanAttackBehavior
{
    class StandardCanAttackBehavior : ICanAttackBehavior
    {
        public bool CanAttack(PlayableUnit source, PlayableUnit target)
        {
            //Если атакуемый юнит жив
            return !target.IsDead &&
                   //игрок не пытается атаковать сам себя
                   source.Player != target.Player &&
                   //и координаты входят в радиус атаки - везвраащем true, иначе - false
                   Math.Abs(source.UnitCoordinates.X - target.UnitCoordinates.X) <= source.MaxAttackRange &&
                   Math.Abs(source.UnitCoordinates.Y - target.UnitCoordinates.Y) <= source.MaxAttackRange;
        }
    }
}
