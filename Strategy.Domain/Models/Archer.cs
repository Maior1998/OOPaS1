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
        private static readonly ImageSource Image =
            new BitmapImage(new Uri("Resources/Units/Archer.png", UriKind.Relative));

        /// <summary>
        ///     Инициализирует нового лучника, управляемого заданным игроком.
        /// </summary>
        /// <param name="Player">Игрок, к которому привязан лучник.</param>
        public Archer(Player Player) : base(Player)
        {
            MaxMoveRange = 3;
            MaxAttackRange = 5;
            Hp = 50;
            Damage = 50;
        }

        /// <summary>
        ///     Инициализирует нового лучника, управляемого заданным игроком и расположенным на заданных координатах.
        /// </summary>
        /// <param name="Player">Игрок, к которому привязан лучник.</param>
        /// <param name="X">Координата X позиции лучника.</param>
        /// <param name="Y">Координата Y позиции лучника.</param>
        public Archer(Player Player, int X, int Y) : this(Player)
        {
            UnitCoordinates = new Coordinates(X, Y);
        }

        public override ImageSource UnitImageSource => IsDead ? DeadUnitSource : Image;

        public override void Attack(PlayableUnit Other)
        {
            if (!CanAtack(Other)) return;
            int Dx = Math.Abs(UnitCoordinates.X - Other.UnitCoordinates.X);
            int Dy = Math.Abs(UnitCoordinates.Y - Other.UnitCoordinates.Y);
            Other.Hp = Math.Max(0, Other.Hp - (Dx <= 1 && Dy <= 1 ? Damage / 2 : Damage));
        }
    }
}