using System;

namespace Strategy.Domain.Models
{
    /// <summary>
    ///     Координаты на карте.
    /// </summary>
    public sealed class Coordinates
    {
        /// <inheritdoc />
        public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
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
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Coordinates Other && Equals(Other);
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
        private bool Equals(Coordinates other)
        {
            return X == other.X && Y == other.Y;
        }

        public static bool operator ==(Coordinates first, Coordinates second)
        {
            return first?.Equals(second) ?? second is null;
        }

        public static bool operator !=(Coordinates first, Coordinates second)
        {
            if (first is null) return !(second is null);
            return !first.Equals(second);
        }

        /// <summary>
        ///     Расчет расстояния между двумя заданными координатами.
        /// </summary>
        /// <param name="first">Первая точка-координата.</param>
        /// <param name="second">Вторая точка-координата.</param>
        /// <returns>Координата - дистанция между заданными.</returns>
        public static Coordinates DistanceTo(Coordinates first, Coordinates second)
        {
            return new Coordinates(Math.Abs(first.X - second.X), Math.Abs(first.Y - second.Y));
        }

        /// <summary>
        ///     Расчет расстояния между текущей координатой и заданной.
        /// </summary>
        /// <param name="other">Координата, до которой необходимо рассчитать расстояние.</param>
        /// <returns>Координата - дистанция между заданными.</returns>
        public Coordinates DistanceTo(Coordinates other)
        {
            return DistanceTo(this, other);
        }
    }
}