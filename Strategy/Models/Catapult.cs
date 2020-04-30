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
        public Catapult(Player player) : base(player,
            new RangeAttack(), 
            new StandardWalk(), 
            new NoStandWithOthers(), 
            new StandardCanMoveBehavior(), 
            new StandardCanAttackBehavior(), 
            1,
            10,
            75,
            100)
        { }

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
    }
}