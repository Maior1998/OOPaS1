namespace Strategy.Domain.Models
{
    /// <summary>
    /// Проходимая поверхность на земле.
    /// </summary>
    public sealed class Grass
    {
        /// <inheritdoc />
        public Grass()
        {
        }

        /// <summary>
        /// Координата x травы на карте.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата y травы на карте.
        /// </summary>
        public int Y { get; set; }
    }
}