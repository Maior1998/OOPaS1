namespace Strategy.Domain.Models
{
    /// <summary>
    /// Класс мечника.
    /// </summary>
    public sealed class Swordsman : PlayableUnit
    {

        /// <summary>
        /// Инициализирует новый объект мечника, управляемый заданным игроком.
        /// </summary>
        /// <param name="player">Игрок, управляющий мечником.</param>
        public Swordsman(Player player) : base(player)
        {
            MaxMoveDX = MaxAttackDY = 5;
            MaxAttackDX = MaxAttackDY = 1;
            HP = 100;
            Damage = 50;
        }
    }
}