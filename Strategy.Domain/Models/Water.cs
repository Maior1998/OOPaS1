using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Strategy.Domain.Models
{
    //TODO: так вода или поверхность? это такая отсылка к Battletoads?
    /// <summary>
    ///     Непроходимая наземная поверхность.
    /// </summary>
    public sealed class Water : GameUnit

    {
        /// <summary>
        ///     Изображение воды.
        /// </summary>
        private static readonly ImageSource Image =
            new BitmapImage(new Uri("Resources/Units/Water.png", UriKind.Relative));

        /// <summary>
        ///     Создает новый объект воды.
        /// </summary>
        public Water()
        {
        }

        /// <summary>
        ///     Создает новый объект воды по указанным координатам.
        /// </summary>
        /// <param name="X">Координата X воды.</param>
        /// <param name="Y">Координата Y воды.</param>
        public Water(int X, int Y)
        {
            UnitCoordinates = new Coordinates(X, Y);
        }

        public override ImageSource UnitImageSource => Image;
    }
}