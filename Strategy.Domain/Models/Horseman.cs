using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Strategy.Domain.Models
{
    /// <summary>
    ///     Класс всадника.
    /// </summary>
    public sealed class Horseman : PlayableUnit
    {
        /// <summary>
        ///     Изображение живого всадника.
        /// </summary>
        private static readonly ImageSource Image =
            new BitmapImage(new Uri("Resources/Units/Horseman.png", UriKind.Relative));

        /// <summary>
        ///     Инициализирует новый объект всадника, управляемый заданным игроком.
        /// </summary>
        /// <param name="Player">Игрок, упровляющий всадником.</param>
        public Horseman(Player Player) : base(Player)
        {
            MaxMoveRange = 10;
            MaxAttackRange = 1;
            Hp = 200;
            Damage = 75;
        }

        /// <summary>
        ///     Инициализирует нового всадника, управляемого заданным игроком и расположенного на заданных координатах.
        /// </summary>
        /// <param name="Player">Игрок, к которому привязан всадник.</param>
        /// <param name="X">Координата X позиции всадника.</param>
        /// <param name="Y">Координата Y позиции всадника.</param>
        public Horseman(Player Player, int X, int Y) : this(Player)
        {
            UnitCoordinates = new Coordinates(X, Y);
        }

        public override ImageSource UnitImageSource => IsDead ? DeadUnitSource : Image;

        public override void Attack(PlayableUnit Other)
        {
            if (!CanAtack(Other)) return;
            Other.Hp = Math.Max(0, Other.Hp - Damage);
        }
    }
}