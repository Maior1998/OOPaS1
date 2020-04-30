using System.Windows.Media;

namespace Strategy.Models
{
    /// <summary>
    ///     Представляет поля всех игровых юнитов, как играбельных, так и неиграбельных.
    /// </summary>
    public abstract class GameUnit
    {
        /// <summary>
        ///     Координаты текущего объекта.
        /// </summary>
        public Coordinates UnitCoordinates;

        /// <summary>
        ///     Изображение текущего юнита.
        /// </summary>
        public abstract ImageSource UnitImageSource { get; }
    }
}