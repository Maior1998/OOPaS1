using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Strategy.Domain.Models
{
    /// <summary>
    /// Представляет поля юнитов, которыми может управлять игрок.
    /// </summary>
    public abstract class PlayableUnit : GameUnit
    {
        /// <summary>
        /// Инициализирует новый игровой юнит.
        /// </summary>
        /// <param name="player">Игрок, управляющий юнитом.</param>
        protected PlayableUnit(Player player)
        {
            Player = player;
        }

        /// <summary>
        /// Инициализирует новый игровой юнит с заданными параметрами.
        /// </summary>
        /// <param name="player">Игрок, управляющий юнитом.</param>
        /// <param name="x">Координата X создаваемого юнита.</param>
        /// <param name="y">Координата Y создаваемого юнита.</param>
        protected PlayableUnit(Player player, int x, int y) : this(player)
        {
            UnitCoordinates.X = x;
            UnitCoordinates.Y = y;
        }

        /// <summary>
        /// Игрок, управляющий юнитом.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Определяет максимальную длину перемещения юнита.
        /// </summary>
        public int MaxMove { get; protected set; }

        /// <summary>
        /// Определяет максимальную длину атаки юнита.
        /// </summary>
        public int MaxAttack { get; protected set; }


        /// <summary>
        /// Урон, наносимый юнитом.
        /// </summary>
        public int Damage { get; protected set; }

        /// <summary>
        /// Очки здоровья юнита.
        /// </summary>
        public int HP { get; set; }

        /// <summary>
        /// Определяет, не умер ли юнит.
        /// </summary>
        public bool IsDead => HP != 0;

        /// <summary>
        /// Изображение мертвого юнита. Применимо только для играбельных юнитов.
        /// </summary>
        public static ImageSource DeadUnitImageSource;

        /// <summary>
        /// Попытаться атаковать другой играбельный юнит.
        /// </summary>
        /// <param name="Other"></param>
        public abstract void Attack(PlayableUnit Other);

        /// <summary>
        /// Определяет, может ли текущий юнит атаковать указанный юнит.
        /// </summary>
        /// <param name="Other"></param>
        /// <returns></returns>
        public bool CanAtack(PlayableUnit Other)
        {
            return Math.Abs(UnitCoordinates.X - Other.UnitCoordinates.X) <= MaxAttack &&
                   Math.Abs(UnitCoordinates.Y - Other.UnitCoordinates.Y) <= MaxAttack;
        }

        //public static bool CanMoveTo(object unit, int x, int y) =>  CanMoveTo(unit as PlayableUnit, x, y);

        /// <summary>
        /// Определяет, может ли текущий юнит переместиться в клетку с указанными координатами.
        /// </summary>
        /// <param name="x">Координата x проверяемой клетки поля.</param>
        /// <param name="y">Координата y проверяемой клетки поля.</param>
        /// <returns></returns>
        public bool CanMoveTo(int x, int y) => CanMoveTo(this, x, y);

        /// <summary>
        /// Определяет, может ли указанный юнит переместиться в клетку с указанными координатами.
        /// </summary>
        /// <param name="unit">Юнит, для которого необходимо проверить возможность перемещения.</param>
        /// <param name="x">Координата x проверяемой клетки поля.</param>
        /// <param name="y">Координата y проверяемой клетки поля.</param>
        /// <returns></returns>
        public static bool CanMoveTo(PlayableUnit unit, int x, int y)
        {
            if (unit == null) return false;
            return Math.Abs(unit.UnitCoordinates.X - x) <= unit.MaxMove &&
                   Math.Abs(unit.UnitCoordinates.Y - y) <= unit.MaxMove;
        }

        /// <summary>
        /// Попытка передвинуть текущий юнит на заданную клекту.
        /// </summary>
        /// <param name="x">Координата x клетки перемещения.</param>
        /// <param name="y">Координата y клетки перемещения.</param>
        public void MoveTo(int x, int y) => MoveTo(this, x, y);

        /// <summary>
        /// Попытка передвинуть указанный юнит на заданную клекту.
        /// </summary>
        /// <param name="Target">Передвигаемый юнит.</param>
        /// <param name="x">Координата x клетки перемещения.</param>
        /// <param name="y">Координата y клетки перемещения.</param>
        public static void MoveTo(PlayableUnit Target, int x, int y)
        {
            if (Target == null) return;
            if (!Target.CanMoveTo(x, y)) return;
            Target.UnitCoordinates.X = x;
            Target.UnitCoordinates.Y = y;
        }

    }
}
