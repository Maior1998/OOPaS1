using System;

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
            MaxMove = 10;
            MaxAttack = 1;
            HP = 200;
            Damage = 75;
        }

        /// <summary>
        /// Инициализирует нового всадника, управляемого заданным игроком и расположенного на заданных координатах.
        /// </summary>
        /// <param name="player">Игрок, к которому привязан всадник.</param>
        /// <param name="x">Координата X позиции всадника.</param>
        /// <param name="y">Координата Y позиции всадника.</param>
        public Horseman(Player player, int x, int y) : this(player)
        {
            UnitCoordinates = new Coordinates(x, y);
        }

        //TODO: проверки на NULL?
        public override void Attack(PlayableUnit Other)
        {
            if (!CanAtack(Other)) return;
            Other.HP = Math.Max(0, Other.HP - Damage);

        }

    }
}