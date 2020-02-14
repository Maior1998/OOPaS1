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
        /// Инициализирует новый игровой юнит с заданными параметрами.
        /// </summary>
        /// <param name="player">Игрок, управляющий юнитом.</param>
        /// <param name="maxdx">Максимальная дистанция ходьбы юнита по горизонтали.</param>
        /// <param name="maxdy">Максимальная дистанция ходьбы юнита по вертикали.</param>
        protected PlayableUnit(Player player)
        {
            Player = player;
        }

        /// <summary>
        /// Игрок, управляющий юнитом.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Определяет максимальную длину перемещения юнита по горизонтали.
        /// </summary>
        public int MaxMoveDX { get; protected set; }

        /// <summary>
        /// Определяет максимальную длину перемещения юнита по вертикали.
        /// </summary>
        public int MaxMoveDY { get; protected set; }

        /// <summary>
        /// Определяет максимальную длину атаки юнита по горизонтали.
        /// </summary>
        public int MaxAttackDX { get; protected set; }

        /// <summary>
        /// Определяет максимальную длину атаки юнита по вертикали.
        /// </summary>
        public int MaxAttackDY { get; protected set; }

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
        /// ПОпытаться атаковать другой юнит.
        /// </summary>
        /// <param name="Other"></param>
        public abstract void Attack(PlayableUnit Other);

        public static bool CanMoveTo(object unit, int x, int y) =>  CanMoveTo(unit as PlayableUnit, x, y);
        
        public static bool CanMoveTo(PlayableUnit unit, int x, int y)
        {
            if (unit == null) return false;
            return Math.Abs(unit.UnitCoordinates.X - x) <= unit.MaxMoveDX &&
                   Math.Abs(unit.UnitCoordinates.Y - y) <= unit.MaxMoveDY;
        }

    }
}
