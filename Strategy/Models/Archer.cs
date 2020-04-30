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
        /// <param name="player">Игрок, к которому привязан лучник.</param>
        public Archer(Player player) : base(
            player,
            new RangeAttack(), 
            new StandardWalk(), 
            new NoStandWithOthers(), 
            new StandardCanMoveBehavior(), 
            new StandardCanAttackBehavior(), 
            3,
            5,
            50,
            50)
        { }

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

        public override ImageSource UnitImageSource => IsDead ? DeadUnitSource : Image;

    }
}