using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Strategy.Domain.Models
{
    /// <summary>
    ///     Проходимая поверхность на земле.
    /// </summary>
    public sealed class Grass : GameUnit
    {
        /// <summary>
        ///     Изображение поверхности.
        /// </summary>
        private static readonly ImageSource Image =
            new BitmapImage(new Uri("Resources/Units/Grass.png", UriKind.Relative));

        /// <summary>
        ///     Создает новый объект травы.
        /// </summary>
        public Grass()
        {
            UnitCoordinates=new Coordinates(0,0);
        }

        /// <summary>
        ///     Создает новый объект травы по указанным координатам.
        /// </summary>
        /// <param name="X">Координата X травы.</param>
        /// <param name="Y">Координата Y травы.</param>
        public Grass(int X, int Y)
        {
            UnitCoordinates = new Coordinates(X, Y);
        }

        public override ImageSource UnitImageSource => Image;
    }
}