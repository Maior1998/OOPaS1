using System;

namespace Strategy.Domain.Models
{
    /// <summary>
    /// Координаты на карте.
    /// </summary>
    public sealed class Coordinates
    {
        /// <summary>
        /// Координата X.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата Y.
        /// </summary>
        public int Y { get; set; }

        /// <inheritdoc />
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Coordinates other && Equals(other);
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
        /// Проверить на равенство с другим объектом.
        /// </summary>
        private bool Equals(Coordinates other)
        {
            return X == other.X && Y == other.Y;
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
        /// Расчет расстояния между двумя заданными координатами.
        /// </summary>
        /// <param name="First">Первая точка-координата.</param>
        /// <param name="Second">Вторая точка-координата.</param>
        /// <returns>Координата - дистанция между заданными.</returns>
        public static Coordinates DistanceTo(Coordinates First, Coordinates Second)
        {
            return new Coordinates(Math.Abs(First.X-Second.X), Math.Abs(First.Y - Second.Y));
        }

        /// <summary>
        /// Расчет расстояния между текущей координатой и заданной.
        /// </summary>
        /// <param name="Other">Координата, до которой необходимо рассчитать расстояние.</param>
        /// <returns>Координата - дистанция между заданными.</returns>
        public Coordinates DistanceTo(Coordinates Other)
        {
            return DistanceTo(this, Other);
        }

    }
}