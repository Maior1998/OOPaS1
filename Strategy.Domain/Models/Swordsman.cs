using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Strategy.Domain.Models
{
    /// <summary>
    ///     Класс мечника.
    /// </summary>
    public sealed class Swordsman : PlayableUnit
    {
        /// <summary>
        ///     Изображение живого мечника.
        /// </summary>
        private static readonly ImageSource image =
            new BitmapImage(new Uri("Resources/Units/Swordsman.png", UriKind.Relative));

        /// <summary>
        ///     Инициализирует новый объект мечника, управляемый заданным игроком.
        /// </summary>
        /// <param name="player">Игрок, управляющий мечником.</param>
        public Swordsman(Player player) : base(player)
        {
            MaxMoveRange = 5;
            MaxAttackRange = 1;
            HP = 100;
            Damage = 50;
        }

        /// <summary>
        ///     Инициализирует нового мечника, управляемого заданным игроком и расположенного на заданных координатах.
        /// </summary>
        /// <param name="player">Игрок, к которому привязан мечник.</param>
        /// <param name="x">Координата X позиции мечника.</param>
        /// <param name="y">Координата Y позиции мечника.</param>
        public Swordsman(Player player, int x, int y) : this(player)
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