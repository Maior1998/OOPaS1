using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Strategy.Domain.Models
{
    /// <summary>
    ///     Катапульта.
    /// </summary>
    public sealed class Catapult : PlayableUnit
    {
        /// <summary>
        ///     Изображение живой катапульты.
        /// </summary>
        private static readonly ImageSource Image =
            new BitmapImage(new Uri("Resources/Units/Catapult.png", UriKind.Relative));

        /// <summary>
        ///     Инициализирует новый объект катапульты, управляемый заданным игроком.
        /// </summary>
        /// <param name="Player">Игрок, управляющий катапультой.</param>
        public Catapult(Player Player) : base(Player)
        {
            MaxMoveRange = 1;
            MaxAttackRange = 10;
            Hp = 75;
            Damage = 100;
        }

        /// <summary>
        ///     Инициализирует новую катапульту, управляемую заданным игроком и расположенную на заданных координатах.
        /// </summary>
        /// <param name="Player">Игрок, к которому привязана катапульта.</param>
        /// <param name="X">Координата X позиции катапульты.</param>
        /// <param name="Y">Координата Y позиции катапульты.</param>
        public Catapult(Player Player, int X, int Y) : this(Player)
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