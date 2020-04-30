using System;
using System.Windows.Media;
using Strategy.Models;

namespace Strategy
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
            if (targetObject is GameUnit unit)
                return unit.UnitCoordinates;
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
            Coordinates moveTargetCoordinates = new Coordinates(x, y);
            if (!(unit is PlayableUnit playableUnit))
            { 
                throw new ArgumentException("Неизвестный тип");
            }

            //проверка, не находится ли в указанной клетке вода.
            foreach (object curObject in currentMap.Ground)
                if (curObject is Water foundedWater 
                    && foundedWater.UnitCoordinates == moveTargetCoordinates)
                    return false;

            //проверка, не находится ли в указанной клетке еще один юнит.
            foreach (object curUnit in currentMap.Units)
            {
                if (!(curUnit is PlayableUnit curPlayableUnit))
                    throw new ArgumentException("Неизвестный тип");
                if (curPlayableUnit.UnitCoordinates == moveTargetCoordinates&&
                    !playableUnit.CanStandWithOtherUnit(curPlayableUnit))
                    return false;
            }
            return playableUnit.CanMove(x, y);
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
            if (!(targetUnit is PlayableUnit targetPlayableUnit) ||
                !(attackingUnit is PlayableUnit attackingPlayableUnit))
                throw new ArgumentException("Неизвестный тип");
            //возвращаем да, если атакующий игрок не пытается ударить сам себя и если может атаковать цель
            return attackingPlayableUnit.CanAtack(targetPlayableUnit);
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

            if (!(attackingUnit is PlayableUnit attackingPlayableUnit) ||
                !(targetUnit is PlayableUnit targetPlayableUnit))
                throw new ArgumentException("Неизвестный тип");
            attackingPlayableUnit.Attack(targetPlayableUnit);
        }

        /// <summary>
        ///     Получить изображение объекта.
        /// </summary>
        public ImageSource GetObjectSource(object targetObject)
        {
            if (targetObject is GameUnit targetGameunit)
                return targetGameunit.UnitImageSource;
            throw new ArgumentException("Неизвестный тип");
        }
    }
}