using System;

namespace Strategy.Domain.Models
{
    /// <summary>
    /// Лучник.
    /// </summary>
    public sealed class Archer : PlayableUnit
    {

        /// <summary>
        /// Инициализирует нового лучника, управляемого заданным игроком.
        /// </summary>
        /// <param name="player">Игрок, к которому привязан лучник.</param>
        public Archer(Player player) : base(player)
        {
            MaxMoveDX = MaxMoveDY = 3;
            MaxAttackDX = MaxAttackDY = 5;
            HP = 50;
            Damage = 50;
        }


        public override void Attack(PlayableUnit Other)
        {
            
            int dx = Math.Abs(UnitCoordinates.X - Other.UnitCoordinates.X);
            int dy = Math.Abs(UnitCoordinates.Y - Other.UnitCoordinates.Y);


        }
    }
}
