using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Strategy.Domain.Models
{
    /// <summary>
    ///     Лучник.
    /// </summary>
    public sealed class Archer : PlayableUnit
    {
        /// <summary>
        ///     Изображение живого лучника.
        /// </summary>
        private static readonly ImageSource image =
            new BitmapImage(new Uri("Resources/Units/Archer.png", UriKind.Relative));

        /// <summary>
        ///     Инициализирует нового лучника, управляемого заданным игроком.
        /// </summary>
        /// <param name="player">Игрок, к которому привязан лучник.</param>
        public Archer(Player player) : base(player)
        {
            MaxMoveRange = 3;
            MaxAttackRange = 5;
            HP = 50;
            Damage = 50;
        }

        /// <summary>
        ///     Инициализирует нового лучника, управляемого заданным игроком и расположенным на заданных координатах.
        /// </summary>
        /// <param name="player">Игрок, к которому привязан лучник.</param>
        /// <param name="x">Координата X позиции лучника.</param>
        /// <param name="y">Координата Y позиции лучника.</param>
        public Archer(Player player, int x, int y) : this(player)
        {
            UnitCoordinates = new Coordinates(x, y);
        }

        public override ImageSource UnitImageSource => IsDead ? _deadUnitSource : image;

        public override void Attack(PlayableUnit Other)
        {
            if (!CanAtack(Other)) return;
            int dx = Math.Abs(UnitCoordinates.X - Other.UnitCoordinates.X);
            int dy = Math.Abs(UnitCoordinates.Y - Other.UnitCoordinates.Y);
            Other.HP = Math.Max(0, Other.HP - (dx <= 1 && dy <= 1 ? Damage / 2 : Damage));
        }
    }
}