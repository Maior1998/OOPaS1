using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy.Domain.Models
{
    /// <summary>
    /// Представляет поля всех игровых юнитов.
    /// </summary>
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
        
    }
}
