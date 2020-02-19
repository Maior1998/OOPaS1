using System;

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
            MaxMove = 1;
            MaxAttack = 10;
            HP = 75;
            Damage = 100;
        }

        /// <summary>
        /// Инициализирует новую катапульту, управляемую заданным игроком и расположенную на заданных координатах.
        /// </summary>
        /// <param name="player">Игрок, к которому привязана катапульта.</param>
        /// <param name="x">Координата X позиции катапульты.</param>
        /// <param name="y">Координата Y позиции катапульты.</param>
        public Catapult(Player player, int x, int y) :this(player)
        {
            UnitCoordinates=new Coordinates(x,y);
        }
        //TODO: проверки на NULL?
        public override void Attack(PlayableUnit Other)
        {
            if (!CanAtack(Other)) return;
            int dx = Math.Abs(UnitCoordinates.X - Other.UnitCoordinates.X);
            int dy = Math.Abs(UnitCoordinates.Y - Other.UnitCoordinates.Y);
            Other.HP = Math.Max(0, Other.HP - (dx <= 1 && dy <= 1 ? Damage / 2 : Damage));

        }

    }
}