using System.Collections.Generic;

namespace Strategy.Domain.Models
{
    /// <summary>
    /// Карта.
    /// </summary>
    public sealed class Map
    {
        /// <inheritdoc />
        public Map(IReadOnlyList<object> ground, IReadOnlyList<object> units)
        {
            Ground = ground;
            Units = units;
        }


        /// <summary>
        /// Поверхность под ногами.
        /// </summary>
        public IReadOnlyList<object> Ground { get; }

        /// <summary>
        /// Список юнитов.
        /// </summary>
        public IReadOnlyList<object> Units { get; }
    }
}