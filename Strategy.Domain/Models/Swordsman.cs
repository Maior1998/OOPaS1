namespace Strategy.Domain.Models
{
    /// <summary>
    /// Класс мечника.
    /// </summary>
    public sealed class Swordsman
    {
        public Swordsman(Player player)
        {
            Player = player;
        }


        /// <summary>
        /// Координата x мечника на карте.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата y мечника на карте.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Игрок, который управляет юнитом.
        /// </summary>
        public Player Player { get; }
    }
}