namespace Strategy.Domain.Models
{
    /// <summary>
    /// Непроходимая наземная поверхность.
    /// </summary>
    public sealed class Water
    {
        /// <summary>
        /// Создает новый пустой объект воды.
        /// </summary>
        public Water()
        {
        }

        /// <summary>
        /// Координата x воды на карте.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата y воды на карте.
        /// </summary>
        public int Y { get; set; }
    }
}