using System.Windows.Media;

namespace Strategy.Models
{
    /// <summary>
    ///     Игрок.
    /// </summary>
    public sealed class Player
    {
        /// <summary>
        ///     Инициализирует новый объект игрока с заданным номером, именем и фоткой (портретом).
        /// </summary>
        /// <param name="id">Номер создаваемого игрока.</param>
        /// <param name="name">Имя создаваемого игрока.</param>
        /// <param name="portrait">Портрет, соответсвующий игроку.</param>
        public Player(int id, string name, ImageSource portrait)
        {
            Id = id;
            Name = name;
            Portrait = portrait;
        }


        /// <summary>
        ///     Идентификатор игрока.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Имя игрока.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Портрет игрока.
        /// </summary>
        public ImageSource Portrait { get; set; }
    }
}