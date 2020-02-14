namespace Strategy.Domain.Models
{
    /// <summary>
    /// Лучник.
    /// </summary>
    public sealed class Archer
    {
        /// <summary>
        /// Инициализирует нового лучника, управляемого заданным игроком
        /// </summary>
        /// <param name="player">Игрок, к которому привязан лучник.</param>
        public Archer(Player player)
        {
            Player = player;
        }


        /// <summary>
        /// Координата x лучника на карте.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата y лучника на карте.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Игрок, который управляет юнитом.
        /// </summary>
        public Player Player { get; }
    }
}