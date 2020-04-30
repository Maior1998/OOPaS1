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
        /// <param name="player">Игрок, упровляющий всадником.</param>
        public Horseman(Player player) : base(player,
            new MeleeAttack(), 
            new StandardWalk(), 
            new NoStandWithOthers(), 
            new StandardCanMoveBehavior(), 
            new StandardCanAttackBehavior(), 
            10,
            1,
            200,
            75)
        { }

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

        public override ImageSource UnitImageSource => IsDead ? DeadUnitSource : Image;
    }
}