using System.Windows.Media;

namespace Strategy.Domain.Models
{
    /// <summary>
    ///     Игрок.
    /// </summary>
    public sealed class Player
    {
        /// <summary>
        ///     Инициализирует новый объект игрока с заданным номером, именем и фоткой (портретом).
        /// </summary>
        /// <param name="Id">Номер создаваемого игрока.</param>
        /// <param name="Name">Имя создаваемого игрока.</param>
        /// <param name="Portrait">Портрет, соответсвующий игроку.</param>
        public Player(int Id, string Name, ImageSource Portrait)
        {
            this.Id = Id;
            this.Name = Name;
            this.Portrait = Portrait;
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