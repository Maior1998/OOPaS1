namespace Strategy.Domain.Models
{
    /// <summary>
    /// Класс всадника.
    /// </summary>
    public sealed class Horseman : PlayableUnit
    {
        /// <summary>
        /// Инициализирует новый объект всадника, управляемый заданным игроком.
        /// </summary>
        /// <param name="player">Игрок, упровляющий всадником.</param>
        public Horseman(Player player) : base(player)
        {
            MaxMoveDX = MaxMoveDY = 10;
            MaxAttackDX = MaxAttackDY = 1;
        }

    }
}