namespace Strategy.Domain.Models
{
    /// <summary>
    /// Проходимая поверхность на земле.
    /// </summary>
    public sealed class Grass : GameUnit
    {
        /// <summary>
        /// Создает новый объект травы.
        /// </summary>
        public Grass()
        {
        }

        /// <summary>
        /// Создает новый объект травы по указанным координатам.
        /// </summary>
        /// <param name="x">Координата X травы.</param>
        /// <param name="y">Координата Y травы.</param>
        public Grass(int x, int y)
        {
            UnitCoordinates=new Coordinates(x,y);
        }
        
    }
}