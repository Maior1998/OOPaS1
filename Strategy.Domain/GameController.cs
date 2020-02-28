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
        private readonly Map currentMap;

        /// <summary>
        ///     Инициализирует новый объект контроллера игры.
        /// </summary>
        /// <param name="currentMap"></param>
        public GameController(Map currentMap)
        {
            this.currentMap = currentMap;
        }

        /// <summary>
        ///     Получить координаты объекта.
        /// </summary>
        /// <param name="targetObject">Координаты объекта, которые необходимо получить.</param>
        /// <returns>Координата x, координата y.</returns>
        public Coordinates GetObjectCoordinates(object targetObject)
        {
            if (targetObject is GameUnit Unit)
                return Unit.UnitCoordinates;
            throw new ArgumentException("Неизвестный тип");
        }

        /// <summary>
        ///     Может ли юнит передвинуться в указанную клетку.
        /// </summary>
        /// <param name="unit">Юнит.</param>
        /// <param name="x">Координата X клетки.</param>
        /// <param name="y">Координата Y клетки.</param>
        /// <returns>
        ///     <see langvalue="true" />, если юнит может переместиться
        ///     <see langvalue="false" /> - иначе.
        /// </returns>
        public bool CanMoveUnit(object unit, int x, int y)
        {
            Coordinates MoveTargetCoordinates = new Coordinates(x, y);
            if (unit is PlayableUnit PlayableUnit)
            {
                if (Math.Abs(PlayableUnit.UnitCoordinates.X - x) > PlayableUnit.MaxMoveRange ||
                    Math.Abs(PlayableUnit.UnitCoordinates.Y - y) > PlayableUnit.MaxMoveRange)
                    return false;
            }
            else
            {
                throw new ArgumentException("Неизвестный тип");
            }

            //проверка, не находится ли в указанной клетке вода.
            foreach (object CurObject in currentMap.Ground)
                if (CurObject is Water FoundedWater && FoundedWater.UnitCoordinates == MoveTargetCoordinates)
                    return false;

            //проверка, не находится ли в указанной клетке еще один юнит.
            foreach (object CurUnit in currentMap.Units)
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
        /// <param name="unit">Юнит.</param>
        /// <param name="x">Координата X клетки.</param>
        /// <param name="y">Координата Y клетки.</param>
        public void MoveUnit(object unit, int x, int y)
        {
            //если не можем передвинуть юнита - выходим.
            if (!CanMoveUnit(unit, x, y))
                return;
            (unit as PlayableUnit).MoveTo(x, y);
        }

        /// <summary>
        ///     Проверить, может ли один юнит атаковать другого.
        /// </summary>
        /// <param name="attackingUnit">Юнит, который собирается совершить атаку.</param>
        /// <param name="targetUnit">Юнит, который является целью.</param>
        /// <returns>
        ///     <see langvalue="true" />, если атака возможна
        ///     <see langvalue="false" /> - иначе.
        /// </returns>
        public bool CanAttackUnit(object attackingUnit, object targetUnit)
        {
            //если хотя бы один из участников не я/я играбельным юнитом - исключение
            if (!(targetUnit is PlayableUnit TargetPlayableUnit) ||
                !(attackingUnit is PlayableUnit AttackingPlayableUnit))
                throw new ArgumentException("Неизвестный тип");
            //возвращаем да, если атакующий игрок не пытается ударить сам себя и если может атаковать цель
            return AttackingPlayableUnit.CanAtack(TargetPlayableUnit);
        }

        /// <summary>
        ///     Атаковать указанного юнита.
        /// </summary>
        /// <param name="attackingUnit">Юнит, который собирается совершить атаку.</param>
        /// <param name="targetUnit">Юнит, который является целью.</param>
        public void AttackUnit(object attackingUnit, object targetUnit)
        {
            if (!CanAttackUnit(attackingUnit, targetUnit))
                return;

            if (!(attackingUnit is PlayableUnit AttackingPlayableUnit) ||
                !(targetUnit is PlayableUnit TargetPlayableUnit))
                throw new ArgumentException("Неизвестный тип");
            AttackingPlayableUnit.Attack(TargetPlayableUnit);
        }

        /// <summary>
        ///     Получить изображение объекта.
        /// </summary>
        public ImageSource GetObjectSource(object targetObject)
        {
            if (targetObject is GameUnit TargetGameunit)
                return TargetGameunit.UnitImageSource;
            throw new ArgumentException("Неизвестный тип");
        }
    }
}