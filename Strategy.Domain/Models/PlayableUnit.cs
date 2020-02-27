using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Strategy.Domain.Models
{
    /// <summary>
    ///     Представляет поля юнитов, которыми может управлять игрок.
    /// </summary>
    public abstract class PlayableUnit : GameUnit
    {
        /// <summary>
        ///     Изображение мертвого юнита. Применимо только для играбельных юнитов.
        /// </summary>
        protected readonly ImageSource DeadUnitSource =
            new BitmapImage(new Uri("Resources/Units/Dead.png", UriKind.Relative));

        /// <summary>
        ///     Инициализирует новый игровой юнит.
        /// </summary>
        /// <param name="Player">Игрок, управляющий юнитом.</param>
        protected PlayableUnit(Player Player)
        {
            this.Player = Player;
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
        /// <param name="Other"></param>
        public abstract void Attack(PlayableUnit Other);

        /// <summary>
        ///     Определяет, может ли текущий юнит атаковать указанный юнит.
        /// </summary>
        /// <param name="Other"></param>
        /// <returns></returns>
        public bool CanAtack(PlayableUnit Other)
        {
            //Если атакуемый юнит жив
            return !Other.IsDead &&
                   //игрок не пытается атаковать сам себя
                   Player != Other.Player &&
                   //и координаты входят в радиус атаки - везвраащем true, иначе - false
                   Math.Abs(UnitCoordinates.X - Other.UnitCoordinates.X) <= MaxAttackRange &&
                   Math.Abs(UnitCoordinates.Y - Other.UnitCoordinates.Y) <= MaxAttackRange;
        }
        
        /// <summary>
        ///     Попытка передвинуть текущий юнит на заданную клекту.
        /// </summary>
        /// <param name="X">Координата x клетки перемещения.</param>
        /// <param name="Y">Координата y клетки перемещения.</param>
        public void MoveTo(int X, int Y)
        {
            MoveTo(this, X, Y);
        }

        /// <summary>
        ///     Попытка передвинуть указанный юнит на заданную клекту.
        /// </summary>
        /// <param name="Target">Передвигаемый юнит.</param>
        /// <param name="X">Координата x клетки перемещения.</param>
        /// <param name="Y">Координата y клетки перемещения.</param>
        public static void MoveTo(PlayableUnit Target, int X, int Y)
        {
            Target.UnitCoordinates.X = X;
            Target.UnitCoordinates.Y = Y;
        }
    }
}