using System;
using System.Windows.Media;
using Strategy.Domain.Models;

namespace Strategy.Domain
{
    /// <summary>
    ///     Контроллер хода игры.
    /// </summary>
    public class GameController
    {
        private readonly Map CurrentMap;

        /// <summary>
        ///     Инициализирует новый объект контроллера игры.
        /// </summary>
        /// <param name="CurrentMap"></param>
        public GameController(Map CurrentMap)
        {
            this.CurrentMap = CurrentMap;
        }

        /// <summary>
        ///     Получить координаты объекта.
        /// </summary>
        /// <param name="TargetObject">Координаты объекта, которые необходимо получить.</param>
        /// <returns>Координата x, координата y.</returns>
        public Coordinates GetObjectCoordinates(object TargetObject)
        {
            if (TargetObject is GameUnit Unit)
                return Unit.UnitCoordinates;
            throw new ArgumentException("Неизвестный тип");
        }

        /// <summary>
        ///     Может ли юнит передвинуться в указанную клетку.
        /// </summary>
        /// <param name="Unit">Юнит.</param>
        /// <param name="X">Координата X клетки.</param>
        /// <param name="Y">Координата Y клетки.</param>
        /// <returns>
        ///     <see langvalue="true" />, если юнит может переместиться
        ///     <see langvalue="false" /> - иначе.
        /// </returns>
        public bool CanMoveUnit(object Unit, int X, int Y)
        {
            Coordinates MoveTargetCoordinates = new Coordinates(X, Y);
            if (Unit is PlayableUnit PlayableUnit)
            {
                if (Math.Abs(PlayableUnit.UnitCoordinates.X - X) > PlayableUnit.MaxMoveRange ||
                    Math.Abs(PlayableUnit.UnitCoordinates.Y - Y) > PlayableUnit.MaxMoveRange)
                    return false;
            }
            else
            {
                throw new ArgumentException("Неизвестный тип");
            }

            //проверка, не находится ли в указанной клетке вода.
            foreach (object CurObject in CurrentMap.Ground)
                if (CurObject is Water FoundedWater && FoundedWater.UnitCoordinates == MoveTargetCoordinates)
                    return false;

            //проверка, не находится ли в указанной клетке еще один юнит.
            foreach (object CurUnit in CurrentMap.Units)
            {
                if (!(CurUnit is PlayableUnit CurPlayableUnit))
                    throw new ArgumentException("Неизвестный тип");
                if (CurPlayableUnit.UnitCoordinates == MoveTargetCoordinates)
                    return false;
            }

            return true;
        }

        /// <summary>
        ///     Передвинуть юнита в указанную клетку.
        /// </summary>
        /// <param name="Unit">Юнит.</param>
        /// <param name="X">Координата X клетки.</param>
        /// <param name="Y">Координата Y клетки.</param>
        public void MoveUnit(object Unit, int X, int Y)
        {
            //если не можем передвинуть юнита - выходим.
            if (!CanMoveUnit(Unit, X, Y))
                return;
            (Unit as PlayableUnit).MoveTo(X, Y);
        }

        /// <summary>
        ///     Проверить, может ли один юнит атаковать другого.
        /// </summary>
        /// <param name="AttackingUnit">Юнит, который собирается совершить атаку.</param>
        /// <param name="TargetUnit">Юнит, который является целью.</param>
        /// <returns>
        ///     <see langvalue="true" />, если атака возможна
        ///     <see langvalue="false" /> - иначе.
        /// </returns>
        public bool CanAttackUnit(object AttackingUnit, object TargetUnit)
        {
            //если хотя бы один из участников не я/я играбельным юнитом - исключение
            if (!(TargetUnit is PlayableUnit TargetPlayableUnit) ||
                !(AttackingUnit is PlayableUnit AttackingPlayableUnit))
                throw new ArgumentException("Неизвестный тип");
            //возвращаем да, если атакующий игрок не пытается ударить сам себя и если может атаковать цель
            return AttackingPlayableUnit.CanAtack(TargetPlayableUnit);
        }

        /// <summary>
        ///     Атаковать указанного юнита.
        /// </summary>
        /// <param name="AttackingUnit">Юнит, который собирается совершить атаку.</param>
        /// <param name="TargetUnit">Юнит, который является целью.</param>
        public void AttackUnit(object AttackingUnit, object TargetUnit)
        {
            if (!CanAttackUnit(AttackingUnit, TargetUnit))
                return;

            if (!(AttackingUnit is PlayableUnit AttackingPlayableUnit) ||
                !(TargetUnit is PlayableUnit TargetPlayableUnit))
                throw new ArgumentException("Неизвестный тип");
            AttackingPlayableUnit.Attack(TargetPlayableUnit);
        }

        /// <summary>
        ///     Получить изображение объекта.
        /// </summary>
        public ImageSource GetObjectSource(object TargetObject)
        {
            if (TargetObject is GameUnit TargetGameunit)
                return TargetGameunit.UnitImageSource;
            throw new ArgumentException("Неизвестный тип");
        }
    }
}