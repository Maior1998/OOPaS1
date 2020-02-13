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

        /// <inheritdoc />
        public GameController(Map map)
        {
            _map = map;
        }


        /// <summary>
        /// Получить координаты объекта.
        /// </summary>
        /// <param name="o">Координаты объекта, которые необходимо получить.</param>
        /// <returns>Координата x, координата y.</returns>
        public Coordinates GetObjectCoordinates(object o)
        {
            if (o is Archer a)
            {
                return new Coordinates(a.X, a.Y);
            }

            if (o is Catapult c)
            {
                return new Coordinates(c.X, c.Y);
            }

            if (o is Horseman h)
            {
                return new Coordinates(h.X, h.Y);
            }

            if (o is Swordsman s)
            {
                return new Coordinates(s.X, s.Y);
            }

            if (o is Grass g)
            {
                return new Coordinates(g.X, g.Y);
            }

            if (o is Water w)
            {
                return new Coordinates(w.X, w.Y);
            }

            throw new ArgumentException("Неизвестный тип");
        }

        /// <summary>
        /// Может ли юнит передвинуться в указанную клетку.
        /// </summary>
        /// <param name="u">Юнит.</param>
        /// <param name="x">Координата X клетки.</param>
        /// <param name="y">Координата Y клетки.</param>
        /// <returns>
        /// <see langvalue="true" />, если юнит может переместиться
        /// <see langvalue="false" /> - иначе.
        /// </returns>
        public bool CanMoveUnit(object u, int x, int y)
        {
            if (u is Archer a)
            {
                if (Math.Abs(a.X - x) > 3 || Math.Abs(a.Y - y) > 3)
                    return false;
            }
            else if (u is Catapult c)
            {
                if (Math.Abs(c.X - x) > 1 || Math.Abs(c.Y - y) > 1)
                    return false;
            }
            else if (u is Horseman h)
            {
                if (Math.Abs(h.X - x) > 10 || Math.Abs(h.Y - y) > 10)
                    return false;
            }
            else if (u is Swordsman s)
            {
                if (Math.Abs(s.X - x) > 5 || Math.Abs(s.Y - y) > 5)
                    return false;
            }
            else
                throw new ArgumentException("Неизвестный тип");


            foreach (object g in _map.Ground)
            {
                if (g is Water w && w.X == x && w.Y == y)
                {
                    return false;
                }
            }

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
        /// <param name="u">Юнит.</param>
        /// <param name="x">Координата X клетки.</param>
        /// <param name="y">Координата Y клетки.</param>
        public void MoveUnit(object u, int x, int y)
        {
            if (!CanMoveUnit(u, x, y))
                return;

            if (u is Archer a)
            {
                a.X = x;
                a.Y = y;
            }
            else if (u is Catapult c)
            {
                c.X = x;
                c.Y = y;
            }
            else if (u is Horseman h)
            {
                h.X = x;
                h.Y = y;
            }
            else if (u is Swordsman s)
            {
                s.X = x;
                s.Y = y;
            }
            else
                throw new ArgumentException("Неизвестный тип");
        }

        /// <summary>
        /// Проверить, может ли один юнит атаковать другого.
        /// </summary>
        /// <param name="au">Юнит, который собирается совершить атаку.</param>
        /// <param name="tu">Юнит, который является целью.</param>
        /// <returns>
        /// <see langvalue="true" />, если атака возможна
        /// <see langvalue="false" /> - иначе.
        /// </returns>
        public bool CanAttackUnit(object au, object tu)
        {
            Coordinates cr = GetObjectCoordinates(tu);
            Player ptu;
            if (tu is Archer a)
            {
                ptu = a.Player;
            }
            else if (tu is Catapult c)
            {
                ptu = c.Player;
            }
            else if (tu is Horseman h)
            {
                ptu = h.Player;
            }
            else if (tu is Swordsman s)
            {
                ptu = s.Player;
            }
            else
                throw new ArgumentException("Неизвестный тип");

            if (IsDead(tu))
                return false;

            if (au is Archer a1)
            {
                if (a1.Player == ptu)
                    return false;

                int dx = a1.X - cr.X;
                int dy = a1.Y - cr.Y;

                return dx >= -5 && dx <= 5 && dy >= -5 && dy <= 5;
            }

            if (au is Catapult c1)
            {
                if (c1.Player == ptu)
                    return false;

                int dx = c1.X - cr.X;
                int dy = c1.Y - cr.Y;

                return dx >= -10 && dx <= 10 && dy >= -10 && dy <= 10;
            }

            if (au is Horseman h1)
            {
                if (h1.Player == ptu)
                    return false;

                int dx = h1.X - cr.X;
                int dy = h1.Y - cr.Y;

                return (dx == -1 || dx == 0 || dx == 1) &&
                       (dy == -1 || dy == 0 || dy == 1);
            }

            if (au is Swordsman s1)
            {
                if (s1.Player == ptu)
                    return false;

                int dx = s1.X - cr.X;
                int dy = s1.Y - cr.Y;

                return (dx == -1 || dx == 0 || dx == 1) &&
                       (dy == -1 || dy == 0 || dy == 1);
            }

            throw new ArgumentException("Неизвестный тип");
        }

        /// <summary>
        /// Атаковать указанного юнита.
        /// </summary>
        /// <param name="au">Юнит, который собирается совершить атаку.</param>
        /// <param name="tu">Юнит, который является целью.</param>
        public void AttackUnit(object au, object tu)
        {
            if (!CanAttackUnit(au, tu))
                return;

            InitializeUnitHp(tu);
            int thp = _hp[tu];
            Coordinates cr = GetObjectCoordinates(tu);
            int d = 0;

            if (au is Archer a)
            {
                d = 50;

                int dx = a.X - cr.X;
                int dy = a.Y - cr.Y;

                if ((dx == -1 || dx == 0 || dx == 1) &&
                    (dy == -1 || dy == 0 || dy == 1))
                {
                    d /= 2;
                }
            }
            else if (au is Catapult c)
            {
                d = 100;

                int dx = c.X - cr.X;
                int dy = c.Y - cr.Y;

                if ((dx == -1 || dx == 0 || dx == 1) &&
                    (dy == -1 || dy == 0 || dy == 1))
                {
                    d /= 2;
                }
            }
            else if (au is Horseman)
            {
                d = 75;
            }
            else if (au is Swordsman)
            {
                d = 50;
            }
            else
                throw new ArgumentException("Неизвестный тип");

            _hp[tu] = Math.Max(thp - d, 0);
        }

        /// <summary>
        /// Получить изображение объекта.
        /// </summary>
        public ImageSource GetObjectSource(object o)
        {
            if (o is Archer)
            {
                if (IsDead(o))
                    return _deadUnitSource;

                return _archerSource;
            }

            if (o is Catapult)
            {
                if (IsDead(o))
                    return _deadUnitSource;

                return _catapultSource;
            }

            if (o is Horseman)
            {
                if (IsDead(o))
                    return _deadUnitSource;

                return _horsemanSource;
            }

            if (o is Swordsman)
            {
                if (IsDead(o))
                    return _deadUnitSource;

                return _swordsmanSource;
            }

            if (o is Grass)
            {
                return _grassSource;
            }

            if (o is Water)
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