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
            MaxMove = 3;
            MaxAttack = 5;
            HP = 50;
            Damage = 50;
        }

        /// <summary>
        /// Инициализирует нового лучника, управляемого заданным игроком и расположенным на заданных координатах.
        /// </summary>
        /// <param name="player">Игрок, к которому привязан лучник.</param>
        /// <param name="x">Координата X позиции лучника.</param>
        /// <param name="y">Координата Y позиции лучника.</param>
        public Archer(Player player, int x, int y) : this(player)
        {
            UnitCoordinates=new Coordinates(x,y);
        }

        //TODO: проверки на NULL?
        public override void Attack(PlayableUnit Other)
        {
            if (!CanAtack(Other)) return;
            int dx = Math.Abs(UnitCoordinates.X - Other.UnitCoordinates.X);
            int dy = Math.Abs(UnitCoordinates.Y - Other.UnitCoordinates.Y);
            Other.HP = Math.Max(0, Other.HP - (dx <= 1 && dy <= 1 ? Damage / 2: Damage ));

        }
    }
}
