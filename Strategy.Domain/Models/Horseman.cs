namespace Strategy.Domain.Models
{
    /// <summary>
    /// Класс всадника.
    /// </summary>
    public sealed class Horseman
    {
        /// <inheritdoc />
        public Horseman(Player player)
        {
            Player = player;
        }

        /// <summary>
        /// Координата x всадника на карте.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата y всадника на карте.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Игрок, который управляет юнитом.
        /// </summary>
        public Player Player { get; }
    }
}