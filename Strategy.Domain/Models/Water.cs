namespace Strategy.Domain.Models
{
    //TODO: так вода или поверхность? это такая отсылка к Battletoads?
    /// <summary>
    /// Непроходимая наземная поверхность.
    /// </summary>
    public sealed class Water : GameUnit

    {
        /// <summary>
        /// Создает новый объект воды.
        /// </summary>
        public Water()
        {
        }

        /// <summary>
        /// Создает новый объект воды по указанным координатам.
        /// </summary>
        /// <param name="x">Координата X воды.</param>
        /// <param name="y">Координата Y воды.</param>
        public Water(int x, int y)
        {
            UnitCoordinates=new Coordinates(x,y);
        }
    }
}