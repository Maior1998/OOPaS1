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

        // Очки жизни каждого юнита.
        private readonly Dictionary<object, int> _hp = new Dictionary<object, int>();

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
                return new Coordinates(unit.X, unit.Y);

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
            if (unit is PlayableUnit archer)
            {
                if (Math.Abs(archer.X - x) > archer.MaxMoveDX || Math.Abs(archer.Y - y) > archer.MaxMoveDY)
                    return false;
            }
            else
                throw new ArgumentException("Неизвестный тип");


            //проверка, находится ли на указанном месте вода.
            foreach (object g in _map.Ground)
            {
                if (g is Water w && w.X == x && w.Y == y)
                    return false;
            }

            //проверка, не находися ли в указанной клетке еще один юнит.
            foreach (object u1 in _map.Units)
            {
                if (u1 is Archer a1)
                {
                    if (a1.X == x && a1.Y == y)
                        return false;
                }
                else if (u1 is Catapult c1)
                {
                    if (c1.X == x && c1.Y == y)
                        return false;
                }
                else if (u1 is Horseman h1)
                {
                    if (h1.X == x && h1.Y == y)
                        return false;
                }
                else if (u1 is Swordsman s1)
                {
                    if (s1.X == x && s1.Y == y)
                        return false;
                }
                else
                    throw new ArgumentException("Неизвестный тип");
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
            {
                playableunit.X = x;
                playableunit.Y = y;
            }
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
            Coordinates cr = GetObjectCoordinates(TargetUnit);
            Player TargetUnitPlayer;
            if (TargetUnit is PlayableUnit playableunit)
            {
                 TargetUnitPlayer = playableunit.Player;
            }
            else
                throw new ArgumentException("Неизвестный тип");

            if (IsDead(TargetUnit))
                return false;

            if (AttackingUnit is Archer a1)
            {
                if (a1.Player == TargetUnitPlayer)
                    return false;

                int dx = a1.X - cr.X;
                int dy = a1.Y - cr.Y;

                //return dx >= -5 && dx <= 5 && dy >= -5 && dy <= 5;
                return Math.Abs(dx)<=5 && Math.Abs(dy) <= 5;
            }

            if (AttackingUnit is Catapult c1)
            {
                if (c1.Player == TargetUnitPlayer)
                    return false;

                int dx = c1.X - cr.X;
                int dy = c1.Y - cr.Y;

                return Math.Abs(dx) <= 10 && Math.Abs(dy) <= 10;
            }

            if (AttackingUnit is Horseman h1)
            {
                if (h1.Player == TargetUnitPlayer)
                    return false;

                int dx = h1.X - cr.X;
                int dy = h1.Y - cr.Y;

                //return (dx == -1 || dx == 0 || dx == 1) &&
                //       (dy == -1 || dy == 0 || dy == 1);
                return Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1;
            }

            if (AttackingUnit is Swordsman s1)
            {
                if (s1.Player == TargetUnitPlayer)
                    return false;

                int dx = s1.X - cr.X;
                int dy = s1.Y - cr.Y;

                //return (dx == -1 || dx == 0 || dx == 1) &&
                //       (dy == -1 || dy == 0 || dy == 1);
                return Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1;
            }

            throw new ArgumentException("Неизвестный тип");
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

            InitializeUnitHp(TargetUnit);
            int thp = _hp[TargetUnit];
            Coordinates cr = GetObjectCoordinates(TargetUnit);
            int d = 0;

            if (AttackingUnit is Archer a)
            {
                d = 50;

                int dx = a.X - cr.X;
                int dy = a.Y - cr.Y;

                //if ((dx == -1 || dx == 0 || dx == 1) &&
                //    (dy == -1 || dy == 0 || dy == 1))
                if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
                {
                    d /= 2;
                }
            }
            else if (AttackingUnit is Catapult c)
            {
                d = 100;

                int dx = c.X - cr.X;
                int dy = c.Y - cr.Y;

                //if ((dx == -1 || dx == 0 || dx == 1) &&
                //    (dy == -1 || dy == 0 || dy == 1))
                if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
                {
                    d /= 2;
                }
            }
            else if (AttackingUnit is Horseman)
            {
                d = 75;
            }
            else if (AttackingUnit is Swordsman)
            {
                d = 50;
            }
            else
                throw new ArgumentException("Неизвестный тип");

            _hp[TargetUnit] = Math.Max(thp - d, 0);
        }

        /// <summary>
        /// Получить изображение объекта.
        /// </summary>
        public ImageSource GetObjectSource(object target_object)
        {
            if (target_object is Archer)
            {
                if (IsDead(target_object))
                    return _deadUnitSource;

                return _archerSource;
            }

            if (target_object is Catapult)
            {
                if (IsDead(target_object))
                    return _deadUnitSource;

                return _catapultSource;
            }

            if (target_object is Horseman)
            {
                if (IsDead(target_object))
                    return _deadUnitSource;

                return _horsemanSource;
            }

            if (target_object is Swordsman)
            {
                if (IsDead(target_object))
                    return _deadUnitSource;

                return _swordsmanSource;
            }

            if (target_object is Grass)
            {
                return _grassSource;
            }

            if (target_object is Water)
            {
                return _waterSource;
            }

            throw new ArgumentException("Неизвестный тип");
        }

        /// <summary>
        /// Проверить, что указанный юнит умер.
        /// </summary>
        /// <param name="u">Юнит.</param>
        /// <returns>
        /// <see langvalue="true" />, если у юнита не осталось очков здоровья,
        /// <see langvalue="false" /> - иначе.
        /// </returns>
        private bool IsDead(object u)
        {
            if (_hp.TryGetValue(u, out int hp))
                return hp == 0;

            InitializeUnitHp(u);
            return false;
        }

        /// <summary>
        /// Инициализировать здоровье юнита.
        /// </summary>
        /// <param name="u">Юнит.</param>
        private void InitializeUnitHp(object u)
        {
            if (_hp.ContainsKey(u))
                return;

            if (u is Archer)
            {
                _hp.Add(u, 50);
            }
            else if (u is Catapult)
            {
                _hp.Add(u, 70);
            }
            else if (u is Horseman)
            {
                _hp.Add(u, 200);
            }
            else if (u is Swordsman)
            {
                _hp.Add(u, 100);
            }
            else
                throw new ArgumentException("Неизвестный тип");
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