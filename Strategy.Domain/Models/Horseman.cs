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
        private static readonly ImageSource image =
            new BitmapImage(new Uri("Resources/Units/Horseman.png", UriKind.Relative));

        /// <summary>
        ///     Инициализирует новый объект всадника, управляемый заданным игроком.
        /// </summary>
        /// <param name="player">Игрок, упровляющий всадником.</param>
        public Horseman(Player player) : base(player)
        {
            MaxMoveRange = 10;
            MaxAttackRange = 1;
            HP = 200;
            Damage = 75;
        }

        /// <summary>
        ///     Инициализирует нового всадника, управляемого заданным игроком и расположенного на заданных координатах.
        /// </summary>
        /// <param name="player">Игрок, к которому привязан всадник.</param>
        /// <param name="x">Координата X позиции всадника.</param>
        /// <param name="y">Координата Y позиции всадника.</param>
        public Horseman(Player player, int x, int y) : this(player)
        {
            UnitCoordinates = new Coordinates(x, y);
        }

        public override ImageSource UnitImageSource => IsDead ? _deadUnitSource : image;

        public override void Attack(PlayableUnit Other)
        {
            if (!CanAtack(Other)) return;
            Other.HP = Math.Max(0, Other.HP - Damage);
        }
    }
}