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
        /// <param name="player">Игрок, управляющий катапультой.</param>
        public Catapult(Player player) : base(player)
        {
            MaxMoveRange = 1;
            MaxAttackRange = 10;
            Hp = 75;
            Damage = 100;
        }

        /// <summary>
        ///     Инициализирует новую катапульту, управляемую заданным игроком и расположенную на заданных координатах.
        /// </summary>
        /// <param name="player">Игрок, к которому привязана катапульта.</param>
        /// <param name="x">Координата X позиции катапульты.</param>
        /// <param name="y">Координата Y позиции катапульты.</param>
        public Catapult(Player player, int x, int y) : this(player)
        {
            UnitCoordinates = new Coordinates(x, y);
        }

        public override ImageSource UnitImageSource => IsDead ? DeadUnitSource : Image;

        public override void Attack(PlayableUnit other)
        {
            if (!CanAtack(other)) return;
            int Dx = Math.Abs(UnitCoordinates.X - other.UnitCoordinates.X);
            int Dy = Math.Abs(UnitCoordinates.Y - other.UnitCoordinates.Y);
            other.Hp = Math.Max(0, other.Hp - (Dx <= 1 && Dy <= 1 ? Damage / 2 : Damage));
        }
    }
}