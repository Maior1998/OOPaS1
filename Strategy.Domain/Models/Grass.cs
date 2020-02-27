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
        private static readonly ImageSource image =
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
        /// <param name="x">Координата X травы.</param>
        /// <param name="y">Координата Y травы.</param>
        public Grass(int x, int y)
        {
            UnitCoordinates = new Coordinates(x, y);
        }

        public override ImageSource UnitImageSource => image;
    }
}