using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
