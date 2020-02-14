using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy.Domain.Models
{
    public abstract class GameUnit
    {
        /// <summary>
        /// Координата X текущего юнита.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата Y текущего юнита.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Игрок, управляющий юнитом.
        /// </summary>
        public Player Player { get; }
    }
}
