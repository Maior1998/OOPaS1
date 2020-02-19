using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Strategy.Domain.Models;

namespace Strategy.Domain
{
    /// <summary>
    /// Контроллер хода игры.
    /// </summary>
    public class GameController
    {

        private readonly Map _map;

        private readonly ImageSource _archerSource = BuildSourceFromPath("Resources/Units/Archer.png");
        private readonly ImageSource _catapultSource = BuildSourceFromPath("Resources/Units/Catapult.png");
        private readonly ImageSource _horsemanSource = BuildSourceFromPath("Resources/Units/Horseman.png");
        private readonly ImageSource _swordsmanSource = BuildSourceFromPath("Resources/Units/Swordsman.png");
        private readonly ImageSource _deadUnitSource = BuildSourceFromPath("Resources/Units/Dead.png");
        private readonly ImageSource _grassSource = BuildSourceFromPath("Resources/Ground/Grass.png");
        private readonly ImageSource _waterSource = BuildSourceFromPath("Resources/Ground/Water.png");

        /// <summary>
        /// Инициализирует новый объект контроллера игры.
        /// </summary>
        /// <param name="map"></param>
        public GameController(Map map)
        {
            _map = map;
        }

        /// <summary>
        /// Получить координаты объекта.
        /// </summary>
        /// <param name="TargetObject">Координаты объекта, которые необходимо получить.</param>
        /// <returns>Координата x, координата y.</returns>
        public Coordinates GetObjectCoordinates(object TargetObject)
        {
            if (TargetObject is GameUnit unit)
                return unit.UnitCoordinates;
            throw new ArgumentException("Неизвестный тип");
        }

        /// <summary>
        /// Может ли юнит передвинуться в указанную клетку.
        /// </summary>
        /// <param name="unit">Юнит.</param>
        /// <param name="x">Координата X клетки.</param>
        /// <param name="y">Координата Y клетки.</param>
        /// <returns>
        /// <see langvalue="true" />, если юнит может переместиться
        /// <see langvalue="false" /> - иначе.
        /// </returns>
        public bool CanMoveUnit(object unit, int x, int y)
        {
            Coordinates MoveTargetCoordinates = new Coordinates(x, y);
            if (unit is PlayableUnit PlayableUnit)
            {
                if (!PlayableUnit.CanMoveTo(x, y)) return false;
            }
            else
                    throw new ArgumentException("Неизвестный тип");

            //проверка, не находится ли в указанной клетке вода.
            foreach (object CurObject in _map.Ground)
            {
                if (CurObject is Water FoundWater && FoundWater.UnitCoordinates == MoveTargetCoordinates)
                    return false;
            }

            //проверка, не находится ли в указанной клетке еще один юнит.
            foreach (object CurUnit in _map.Units)
            {
                if (!(CurUnit is PlayableUnit CurPlayableUnit)) 
                    throw new ArgumentException("Неизвестный тип");
                if (CurPlayableUnit.UnitCoordinates == MoveTargetCoordinates)
                    return false;
                
            }

            return true;
        }

        /// <summary>
        /// Передвинуть юнита в указанную клетку.
        /// </summary>
        /// <param name="unit">Юнит.</param>
        /// <param name="x">Координата X клетки.</param>
        /// <param name="y">Координата Y клетки.</param>
        public void MoveUnit(object unit, int x, int y)
        {
            //если не можем передвинуть юнита - выходим.
            if (!CanMoveUnit(unit, x, y))
                return;

            if (unit is PlayableUnit playableunit)
                playableunit.MoveTo(x, y);
            else
                throw new ArgumentException("Неизвестный тип");
        }

        /// <summary>
        /// Проверить, может ли один юнит атаковать другого.
        /// </summary>
        /// <param name="AttackingUnit">Юнит, который собирается совершить атаку.</param>
        /// <param name="TargetUnit">Юнит, который является целью.</param>
        /// <returns>
        /// <see langvalue="true" />, если атака возможна
        /// <see langvalue="false" /> - иначе.
        /// </returns>
        public bool CanAttackUnit(object AttackingUnit, object TargetUnit)
        {
            Player TargetUnitPlayer;
            PlayableUnit TargetPlayableUnit = TargetUnit as PlayableUnit;
            if (!(TargetPlayableUnit is null))
                TargetUnitPlayer = TargetPlayableUnit.Player;
            else
                throw new ArgumentException("Неизвестный тип");

            //если юнит уже помер, то и атаковать его нет смысла
            if (TargetPlayableUnit.IsDead)
                return false;

            if (!(AttackingUnit is PlayableUnit AttackingPlayableUnit))
                throw new ArgumentException("Неизвестный тип");

            return AttackingPlayableUnit.Player != TargetUnitPlayer &&
                   AttackingPlayableUnit.CanAtack(TargetPlayableUnit);
        }

        /// <summary>
        /// Атаковать указанного юнита.
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
        /// Получить изображение объекта.
        /// </summary>
        public ImageSource GetObjectSource(object target_object)
        {
            switch (target_object)
            {
                case Archer Archer:
                    return Archer.IsDead ? _deadUnitSource : _archerSource;
                case Catapult Catapult:
                    return Catapult.IsDead ? _deadUnitSource : _catapultSource;
                case Horseman Horseman:
                    return Horseman.IsDead ? _deadUnitSource : _horsemanSource;
                case Swordsman Swordsman:
                    return Swordsman.IsDead ? _deadUnitSource : _swordsmanSource;
                case Grass _:
                    return _grassSource;
                case Water _:
                    return _waterSource;
                default:
                    throw new ArgumentException("Неизвестный тип");
            }
        }

        /// <summary>
        /// Получить изображение по указанному пути.
        /// </summary>
        private static ImageSource BuildSourceFromPath(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }
    }
}