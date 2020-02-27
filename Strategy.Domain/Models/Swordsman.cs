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
        private static readonly ImageSource Image =
            new BitmapImage(new Uri("Resources/Units/Swordsman.png", UriKind.Relative));

        /// <summary>
        ///     Инициализирует новый объект мечника, управляемый заданным игроком.
        /// </summary>
        /// <param name="Player">Игрок, управляющий мечником.</param>
        public Swordsman(Player Player) : base(Player)
        {
            MaxMoveRange = 5;
            MaxAttackRange = 1;
            Hp = 100;
            Damage = 50;
        }

        /// <summary>
        ///     Инициализирует нового мечника, управляемого заданным игроком и расположенного на заданных координатах.
        /// </summary>
        /// <param name="Player">Игрок, к которому привязан мечник.</param>
        /// <param name="X">Координата X позиции мечника.</param>
        /// <param name="Y">Координата Y позиции мечника.</param>
        public Swordsman(Player Player, int X, int Y) : this(Player)
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