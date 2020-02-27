using System.Collections.Generic;

namespace Strategy.Domain.Models
{
    /// <summary>
    ///     Карта.
    /// </summary>
    public sealed class Map
    {
        /// <summary>
        ///     Инициализирует новый объект карты по заданной поверхности и набору юнитов.
        /// </summary>
        /// <param name="Ground">ПОверхность карты.</param>
        /// <param name="Units">Юниты, расположенные на карте.</param>
        public Map(IReadOnlyList<object> Ground, IReadOnlyList<object> Units)
        {
            this.Ground = Ground;
            this.Units = Units;
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