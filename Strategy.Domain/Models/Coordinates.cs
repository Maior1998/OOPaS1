using System;

namespace Strategy.Domain.Models
{
    /// <summary>
    ///     Координаты на карте.
    /// </summary>
    public sealed class Coordinates
    {
        /// <inheritdoc />
        public Coordinates(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        ///     Координата X.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///     Координата Y.
        /// </summary>
        public int Y { get; set; }

        /// <inheritdoc />
        public override bool Equals(object Obj)
        {
            if (ReferenceEquals(null, Obj)) return false;
            if (ReferenceEquals(this, Obj)) return true;
            return Obj is Coordinates Other && Equals(Other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        /// <summary>
        ///     Проверить на равенство с другим объектом.
        /// </summary>
        private bool Equals(Coordinates Other)
        {
            return X == Other.X && Y == Other.Y;
        }

        public static bool operator ==(Coordinates First, Coordinates Second)
        {
            return First?.Equals(Second) ?? Second is null;
        }

        public static bool operator !=(Coordinates First, Coordinates Second)
        {
            if (First is null) return !(Second is null);
            return !First.Equals(Second);
        }

        /// <summary>
        ///     Расчет расстояния между двумя заданными координатами.
        /// </summary>
        /// <param name="First">Первая точка-координата.</param>
        /// <param name="Second">Вторая точка-координата.</param>
        /// <returns>Координата - дистанция между заданными.</returns>
        public static Coordinates DistanceTo(Coordinates First, Coordinates Second)
        {
            return new Coordinates(Math.Abs(First.X - Second.X), Math.Abs(First.Y - Second.Y));
        }

        /// <summary>
        ///     Расчет расстояния между текущей координатой и заданной.
        /// </summary>
        /// <param name="Other">Координата, до которой необходимо рассчитать расстояние.</param>
        /// <returns>Координата - дистанция между заданными.</returns>
        public Coordinates DistanceTo(Coordinates Other)
        {
            return DistanceTo(this, Other);
        }
    }
}