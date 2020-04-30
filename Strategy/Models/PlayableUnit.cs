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
    ///     Представляет поля юнитов, которыми может управлять игрок.
    /// </summary>
    public abstract class PlayableUnit : GameUnit
    {
        protected IAttackBehavior attackBehavior;
        protected IMoveBehavior moveBehavior;
        protected ICanStandWithOtherUnitBehavior canStandWithOtherUnitBehavior;
        protected ICanMoveBehavior canMoveBehavior;
        protected ICanAttackBehavior canAttackBehavior;
        /// <summary>
        ///     Изображение мертвого юнита. Применимо только для играбельных юнитов.
        /// </summary>
        protected readonly ImageSource DeadUnitSource =
            new BitmapImage(new Uri("Resources/Units/Dead.png", UriKind.Relative));

        
        /// <summary>
        /// Инициализирует новый игровой юнит.
        /// </summary>
        /// <param name="player">Игрок, управляющий юнитом.</param>
        /// <param name="_attackBehavior">Поведение атаки юнита.</param>
        /// <param name="_moveBehavior">Поведение передвижения юнита.</param>
        /// <param name="_canStandWithOtherUnitsBehavior">Поведение юнита при нахождении с другими на одной клетке.</param>
        /// <param name="_canMoveBehavior">Поведение юнита при определении, может ли он передвигаться.</param>
        /// <param name="_canAttackBehavior">Поведение юнита при определении, может ли он атаковать.</param>
        /// <param name="maxMoveRange">Дистанция передвижения юнита.</param>
        /// <param name="maxAttackRange">Дистанция атаки юнита.</param>
        /// <param name="hp">Очки здоровья юнита.</param>
        /// <param name="damage">Урон, наносимый юнитом.</param>
        protected PlayableUnit(
            Player player,
            IAttackBehavior _attackBehavior,
            IMoveBehavior _moveBehavior,
            ICanStandWithOtherUnitBehavior _canStandWithOtherUnitsBehavior,
            ICanMoveBehavior _canMoveBehavior,
            ICanAttackBehavior _canAttackBehavior,
            int maxMoveRange,
            int maxAttackRange,
            int hp,
            int damage)
        {
            Player = player;
            attackBehavior = _attackBehavior;
            moveBehavior = _moveBehavior;
            canStandWithOtherUnitBehavior = _canStandWithOtherUnitsBehavior;
            canMoveBehavior = _canMoveBehavior;
            canAttackBehavior = _canAttackBehavior;
            Hp = hp;
            Damage = damage;
            MaxMoveRange = maxMoveRange;
            MaxAttackRange = maxAttackRange;
        }

        /// <summary>
        ///     Игрок, управляющий юнитом.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        ///     Определяет максимальную длину перемещения юнита.
        /// </summary>
        public int MaxMoveRange { get; protected set; }

        /// <summary>
        ///     Определяет максимальную длину атаки юнита.
        /// </summary>
        public int MaxAttackRange { get; protected set; }

        /// <summary>
        ///     Урон, наносимый юнитом.
        /// </summary>
        public int Damage { get; protected set; }

        /// <summary>
        ///     Очки здоровья юнита.
        /// </summary>
        public int Hp { get; set; }

        /// <summary>
        ///     Определяет, не умер ли юнит.
        /// </summary>
        public bool IsDead => Hp == 0;

        /// <summary>
        ///     Попытаться атаковать другой играбельный юнит.
        /// </summary>
        /// <param name="other"></param>
        public void Attack(PlayableUnit other)
        {
            if (!CanAtack(other)) return;
            attackBehavior.Attack(this,other);
        }

        /// <summary>
        ///     Определяет, может ли текущий юнит атаковать указанный юнит.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CanAtack(PlayableUnit other)
        {
            return canAttackBehavior.CanAttack(this, other);
        }
        
        /// <summary>
        ///     Попытка передвинуть текущий юнит на заданную клекту.
        /// </summary>
        /// <param name="x">Координата x клетки перемещения.</param>
        /// <param name="y">Координата y клетки перемещения.</param>
        public void MoveTo(int x, int y)
        {
            moveBehavior.MoveTo(this,x,y);
        }

        public bool CanStandWithOtherUnit(PlayableUnit other)
        {
            return canStandWithOtherUnitBehavior.CanStandWithOtherUnit(other);
        }

        public bool CanMove(int x, int y)
        {
            return canMoveBehavior.CanMove(this, x, y);
        }
    }
}