using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Models;

namespace Strategy.Behaviors.MoveBehaviors
{
    public interface IMoveBehavior
    {
        /// <summary>
        ///     Попытка передвинуть текущий юнит на заданную клекту.
        /// </summary>
        /// <param name="x">Координата x клетки перемещения.</param>
        /// <param name="y">Координата y клетки перемещения.</param>
        void MoveTo(PlayableUnit target, int x, int y);
    }
}
