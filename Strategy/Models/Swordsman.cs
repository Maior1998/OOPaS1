using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Strategy.Behaviors.AttackBehaviors;
using Strategy.Behaviors.CanAttackBehavior;
using Strategy.Behaviors.CanMoveBehaviors;
using Strategy.Behaviors.MoveBehaviors;
using Strategy.Behaviors.StandWithOtherUnitBehaviors;

namespace Strategy.Models
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
        /// <param name="player">Игрок, управляющий мечником.</param>
        public Swordsman(Player player) : base(player,
            new MeleeAttack(), 
            new StandardWalk(), 
            new NoStandWithOthers(), 
            new StandardCanMoveBehavior(), 
            new StandardCanAttackBehavior(), 
            5,
            1,
            100,
            50)
        { }

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

        public override ImageSource UnitImageSource => IsDead ? DeadUnitSource : Image;
    }
}