﻿using System.Collections.Generic;

namespace Strategy.Models
{
    /// <summary>
    ///     Карта.
    /// </summary>
    public sealed class Map
    {
        /// <summary>
        ///     Инициализирует новый объект карты по заданной поверхности и набору юнитов.
        /// </summary>
        /// <param name="ground">ПОверхность карты.</param>
        /// <param name="units">Юниты, расположенные на карте.</param>
        public Map(IReadOnlyList<object> ground, IReadOnlyList<object> units)
        {
            this.Ground = ground;
            this.Units = units;
        }


        /// <summary>
        ///     Поверхность под ногами.
        /// </summary>
        public IReadOnlyList<object> Ground { get; }

        /// <summary>
        ///     Список юнитов.
        /// </summary>
        public IReadOnlyList<object> Units { get; }
    }
}