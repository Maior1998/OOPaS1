namespace Strategy.Domain.Models
{
    /// <summary>
    /// Катапульта.
    /// </summary>
    public sealed class Catapult
    {
        /// <inheritdoc />
        public Catapult(Player player)
        {
            Player = player;
        }


        /// <summary>
        /// Координата x катапульты на карте.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата y катапульты на карте.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Игрок, который управляет юнитом.
        /// </summary>
        public Player Player { get; }
    }
}