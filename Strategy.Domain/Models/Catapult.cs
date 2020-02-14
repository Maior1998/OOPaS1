namespace Strategy.Domain.Models
{
    /// <summary>
    /// Катапульта.
    /// </summary>
    public sealed class Catapult : PlayableUnit
    {
        /// <summary>
        /// Инициализирует новый объект катапульты, управляемый заданным игроком.
        /// </summary>
        /// <param name="player">Игрок, управляющий катапультой.</param>
        public Catapult(Player player):base(player)
        {
            MaxMoveDX = MaxMoveDY = 1;
            MaxAttackDX = MaxAttackDY = 10;
            HP = 75;
            Damage = 100;
        }

    }
}