using System.Collections.Generic;
using NUnit.Framework;
using Strategy.Domain.Models;

namespace Strategy.Domain.Tests
{
    /// <summary>
    ///     Тестирование <see cref="GameController" />.
    /// </summary>
    [TestFixture]
    public class GameControllerTests
    {
        /// <summary>
        ///     Рассчитать количество ударов, которое необходимо, чтобы убить юнита.
        /// </summary>
        /// <param name="GameController">Контроллер игры.</param>
        /// <param name="AttackerUnit">Юнит, который наносит удар.</param>
        /// <param name="TargetUnit">Юнит, который является целью.</param>
        /// <returns>Количество ударов, которое было нанесено юниту.</returns>
        /// <remarks>
        ///     Проверка не точная. Считается какое количество ударов нужно, чтобы убить противника.
        ///     Смерть считается по тому, что больше нельзя атаковать. В общем случае, такая проверка работоспособна.
        /// </remarks>
        private static int GetAttacksCount(GameController GameController, object AttackerUnit, object TargetUnit)
        {
            int Count = 0;
            while (GameController.CanAttackUnit(AttackerUnit, TargetUnit))
            {
                GameController.AttackUnit(AttackerUnit, TargetUnit);
                Count++;
            }

            return Count;
        }

        /// <summary>
        ///     Создать карту.
        /// </summary>
        /// <param name="Ground">Информация о местности.</param>
        /// <param name="Units">Список юнитов.</param>
        private static Map CreateMap(IReadOnlyList<object> Ground = null, IReadOnlyList<object> Units = null)
        {
            return new Map(Ground ?? new object[0], Units ?? new object[0]);
        }

        /// <summary>
        ///     Проверить дальнюю атаку лучника.
        /// </summary>
        [Test]
        public void AttackUnit_ArcherAttackAllTypes()
        {
            Player Player1 = new Player(1, "Игрок №1", null);
            Player Player2 = new Player(2, "Игрок №2", null);
            Archer Archer = new Archer(Player1, 8, 8);
            Map Map = CreateMap();
            GameController GameController = new GameController(Map);


            // Лучник имеет 50 жизней. Погибнет за один удар.
            Archer ArcherTarget = new Archer(Player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(GameController, Archer, ArcherTarget));

            // Катапульта имеет 75 жизней. Погибнет за два удара.
            Catapult CatapultTarget = new Catapult(Player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(GameController, Archer, CatapultTarget));

            // Всадник имеет 200 жизней. Необходимо 4 удара.
            Horseman HorsemanTarget = new Horseman(Player2, 10, 10);
            Assert.AreEqual(4, GetAttacksCount(GameController, Archer, HorsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за два удара.
            Swordsman SwordsmanTarget = new Swordsman(Player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(GameController, Archer, SwordsmanTarget));
        }

        /// <summary>
        ///     Проверить ближнюю атаку лучника.
        /// </summary>
        [Test]
        public void AttackUnit_ArcherAttackCloseCombatAllTypes()
        {
            Player Player1 = new Player(1, "Игрок №1", null);
            Player Player2 = new Player(2, "Игрок №2", null);
            Archer Archer = new Archer(Player1, 9, 9);
            Map Map = CreateMap();
            GameController GameController = new GameController(Map);


            // Лучник имеет 50 жизней. Погибнет за два удара.
            Archer ArcherTarget = new Archer(Player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(GameController, Archer, ArcherTarget));

            // Катапульта имеет 75 жизней. Погибнет за три удара.
            Catapult CatapultTarget = new Catapult(Player2, 10, 10);
            Assert.AreEqual(3, GetAttacksCount(GameController, Archer, CatapultTarget));

            // Всадник имеет 200 жизней. Необходимо 8 ударов.
            Horseman HorsemanTarget = new Horseman(Player2, 10, 10);
            Assert.AreEqual(8, GetAttacksCount(GameController, Archer, HorsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за 4 удара.
            Swordsman SwordsmanTarget = new Swordsman(Player2, 10, 10);
            Assert.AreEqual(4, GetAttacksCount(GameController, Archer, SwordsmanTarget));
        }

        /// <summary>
        ///     Проверить дальнюю атаку катапульты.
        /// </summary>
        [Test]
        public void AttackUnit_CatapultAttackAllTypes()
        {
            Player Player1 = new Player(1, "Игрок №1", null);
            Player Player2 = new Player(2, "Игрок №2", null);
            Catapult Catapult = new Catapult(Player1, 8, 8);
            Map Map = CreateMap();
            GameController GameController = new GameController(Map);


            // Лучник имеет 50 жизней. Погибнет за один удар.
            Archer ArcherTarget = new Archer(Player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(GameController, Catapult, ArcherTarget));

            // Катапульта имеет 75 жизней. Погибнет за один удар.
            Catapult CatapultTarget = new Catapult(Player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(GameController, Catapult, CatapultTarget));

            // Всадник имеет 200 жизней. Необходимо 2 удара.
            Horseman HorsemanTarget = new Horseman(Player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(GameController, Catapult, HorsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за один удар.
            Swordsman SwordsmanTarget = new Swordsman(Player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(GameController, Catapult, SwordsmanTarget));
        }

        /// <summary>
        ///     Проверить ближнюю атаку катапульты.
        /// </summary>
        [Test]
        public void AttackUnit_CatapultAttackCloseCombatAllTypes()
        {
            Player Player1 = new Player(1, "Игрок №1", null);
            Player Player2 = new Player(2, "Игрок №2", null);
            Catapult Catapult = new Catapult(Player1, 9, 9);
            Map Map = CreateMap();
            GameController GameController = new GameController(Map);


            // Лучник имеет 50 жизней. Погибнет за один удар.
            Archer ArcherTarget = new Archer(Player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(GameController, Catapult, ArcherTarget));

            // Катапульта имеет 75 жизней. Погибнет за два удара.
            Catapult CatapultTarget = new Catapult(Player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(GameController, Catapult, CatapultTarget));

            // Всадник имеет 200 жизней. Необходимо 4 удара.
            Horseman HorsemanTarget = new Horseman(Player2, 10, 10);
            Assert.AreEqual(4, GetAttacksCount(GameController, Catapult, HorsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за два удара.
            Swordsman SwordsmanTarget = new Swordsman(Player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(GameController, Catapult, SwordsmanTarget));
        }

        /// <summary>
        ///     Проверить атаку всадника.
        /// </summary>
        [Test]
        public void AttackUnit_HorsemanAttackAllTypes()
        {
            Player Player1 = new Player(1, "Игрок №1", null);
            Player Player2 = new Player(2, "Игрок №2", null);
            Horseman Horseman = new Horseman(Player1, 9, 9);
            Map Map = CreateMap();
            GameController GameController = new GameController(Map);


            // Лучник имеет 50 жизней. Погибнет за один удар.
            Archer ArcherTarget = new Archer(Player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(GameController, Horseman, ArcherTarget));

            // Катапульта имеет 75 жизней. Погибнет за один удар.
            Catapult CatapultTarget = new Catapult(Player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(GameController, Horseman, CatapultTarget));

            // Всадник имеет 200 жизней. Необходимо 3 удара.
            Horseman HorsemanTarget = new Horseman(Player2, 10, 10);
            Assert.AreEqual(3, GetAttacksCount(GameController, Horseman, HorsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за два удара.
            Swordsman SwordsmanTarget = new Swordsman(Player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(GameController, Horseman, SwordsmanTarget));
        }

        /// <summary>
        ///     Проверить атаку мечника.
        /// </summary>
        [Test]
        public void AttackUnit_SwordsmanAttackAllTypes()
        {
            Player Player1 = new Player(1, "Игрок №1", null);
            Player Player2 = new Player(2, "Игрок №2", null);
            Swordsman Swordsman = new Swordsman(Player1, 9, 9);
            Map Map = CreateMap();
            GameController GameController = new GameController(Map);


            // Лучник имеет 50 жизней. Погибнет за один удар.
            Archer ArcherTarget = new Archer(Player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(GameController, Swordsman, ArcherTarget));

            // Катапульта имеет 75 жизней. Погибнет за два удара.
            Catapult CatapultTarget = new Catapult(Player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(GameController, Swordsman, CatapultTarget));

            // Всадник имеет 200 жизней. Необходимо 4 удара.
            Horseman HorsemanTarget = new Horseman(Player2, 10, 10);
            Assert.AreEqual(4, GetAttacksCount(GameController, Swordsman, HorsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за два удара.
            Swordsman SwordsmanTarget = new Swordsman(Player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(GameController, Swordsman, SwordsmanTarget));
        }

        /// <summary>
        ///     Проверить, что лучник может атаковать врага.
        /// </summary>
        [Test]
        [TestCase(4, 5, false)]
        [TestCase(5, 4, false)]
        [TestCase(15, 16, false)]
        [TestCase(16, 15, false)]
        [TestCase(5, 5, true)]
        [TestCase(15, 15, true)]
        [TestCase(9, 10, true)]
        [TestCase(12, 7, true)]
        public void CanAttackUnit_Archer(int X, int Y, bool CanAttack)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player Player1 = new Player(1, "Игрок №1", null);
            Player Player2 = new Player(2, "Игрок №2", null);
            Archer Archer = new Archer(Player1, startPositionX, startPositionY);
            Archer Target = new Archer(Player2, X, Y);
            Map Map = CreateMap(Units: new object[] {Archer, Target});
            GameController GameController = new GameController(Map);

            Assert.AreEqual(CanAttack, GameController.CanAttackUnit(Archer, Target));
        }

        /// <summary>
        ///     Проверить, что невозможна атака дружественного юнита.
        /// </summary>
        [Test]
        public void CanAttackUnit_ArcherAttackFriend_False()
        {
            Player Player = new Player(1, "Игрок №1", null);
            Archer Archer = new Archer(Player, 10, 10);
            Archer Target = new Archer(Player, 11, 11);
            Map Map = CreateMap(Units: new[] {Archer, Target});
            GameController GameController = new GameController(Map);

            Assert.False(GameController.CanAttackUnit(Archer, Target));
        }

        /// <summary>
        ///     Проверить, что катапульта может атаковать врага.
        /// </summary>
        [Test]
        [TestCase(10, 9, false)]
        [TestCase(9, 10, false)]
        [TestCase(30, 31, false)]
        [TestCase(31, 30, false)]
        [TestCase(10, 10, true)]
        [TestCase(30, 30, true)]
        [TestCase(11, 15, true)]
        [TestCase(25, 12, true)]
        public void CanAttackUnit_Catapult(int X, int Y, bool CanAttack)
        {
            const int startPositionX = 20;
            const int startPositionY = 20;

            Player Player1 = new Player(1, "Игрок №1", null);
            Player Player2 = new Player(2, "Игрок №2", null);
            Catapult Catapult = new Catapult(Player1, startPositionX, startPositionY);
            Archer Target = new Archer(Player2, X, Y);
            Map Map = CreateMap(Units: new object[] {Catapult, Target});
            GameController GameController = new GameController(Map);

            Assert.AreEqual(CanAttack, GameController.CanAttackUnit(Catapult, Target));
        }

        /// <summary>
        ///     Проверить, что всадник может атаковать врага.
        /// </summary>
        [Test]
        [TestCase(8, 9, false)]
        [TestCase(9, 8, false)]
        [TestCase(11, 12, false)]
        [TestCase(12, 11, false)]
        [TestCase(11, 11, true)]
        [TestCase(10, 11, true)]
        [TestCase(9, 9, true)]
        [TestCase(9, 10, true)]
        public void CanAttackUnit_Horseman(int X, int Y, bool CanAttack)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player Player1 = new Player(1, "Игрок №1", null);
            Player Player2 = new Player(2, "Игрок №2", null);
            Horseman Horseman = new Horseman(Player1, startPositionX, startPositionY);
            Archer Target = new Archer(Player2, X, Y);
            Map Map = CreateMap(Units: new object[] {Horseman, Target});
            GameController GameController = new GameController(Map);

            Assert.AreEqual(CanAttack, GameController.CanAttackUnit(Horseman, Target));
        }

        /// <summary>
        ///     Проверить, что мечник может атаковать врага.
        /// </summary>
        [Test]
        [TestCase(8, 9, false)]
        [TestCase(9, 8, false)]
        [TestCase(11, 12, false)]
        [TestCase(12, 11, false)]
        [TestCase(11, 11, true)]
        [TestCase(10, 11, true)]
        [TestCase(9, 9, true)]
        [TestCase(9, 10, true)]
        public void CanAttackUnit_Swordsman(int X, int Y, bool CanAttack)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player Player1 = new Player(1, "Игрок №1", null);
            Player Player2 = new Player(2, "Игрок №2", null);
            Swordsman Swordsman = new Swordsman(Player1, startPositionX, startPositionY);
            Archer Target = new Archer(Player2, X, Y);
            Map Map = CreateMap(Units: new object[] {Swordsman, Target});
            GameController GameController = new GameController(Map);

            Assert.AreEqual(CanAttack, GameController.CanAttackUnit(Swordsman, Target));
        }

        /// <summary>
        ///     Проверить перемещение арбалетчика на пустой карте.
        /// </summary>
        [Test]
        [TestCase(6, 7, false)]
        [TestCase(7, 6, false)]
        [TestCase(14, 13, false)]
        [TestCase(13, 14, false)]
        [TestCase(10, 10, false)]
        [TestCase(9, 10, true)]
        [TestCase(11, 10, true)]
        [TestCase(7, 7, true)]
        [TestCase(13, 13, true)]
        public void CanMoveUnit_ArcherOnEmptyMap(int X, int Y, bool CanMove)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player Player = new Player(1, "Игрок №1", null);
            Archer Archer = new Archer(Player, startPositionX, startPositionY);
            Map Map = CreateMap(Units: new[] {Archer});
            GameController GameController = new GameController(Map);

            Assert.AreEqual(CanMove, GameController.CanMoveUnit(Archer, X, Y));
        }

        /// <summary>
        ///     Проверить перемещение катапульты на пустой карте.
        /// </summary>
        [Test]
        [TestCase(8, 9, false)]
        [TestCase(9, 8, false)]
        [TestCase(10, 10, false)]
        [TestCase(11, 12, false)]
        [TestCase(12, 11, false)]
        [TestCase(9, 10, true)]
        [TestCase(11, 10, true)]
        [TestCase(9, 9, true)]
        [TestCase(11, 11, true)]
        public void CanMoveUnit_CatapultOnEmptyMap(int X, int Y, bool CanMove)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player Player = new Player(1, "Игрок №1", null);
            Catapult Catapult = new Catapult(Player, startPositionX, startPositionY);
            Map Map = CreateMap(Units: new[] {Catapult});
            GameController GameController = new GameController(Map);

            Assert.AreEqual(CanMove, GameController.CanMoveUnit(Catapult, X, Y));
        }

        /// <summary>
        ///     Проверить, что юнит не может переместиться на клетку, которую занял другой юнит.
        /// </summary>
        [Test]
        public void CanMoveUnit_CatapultOnHorseman_False()
        {
            const int horsemanPositionX = 15;
            const int horsemanPositionY = 15;

            Player Player = new Player(1, "Игрок №1", null);
            Catapult Catapult = new Catapult(Player, 10, 10);
            Horseman Horseman = new Horseman(Player, horsemanPositionX, horsemanPositionY);
            Map Map = CreateMap(Units: new object[] {Catapult, Horseman});
            GameController GameController = new GameController(Map);

            Assert.False(GameController.CanMoveUnit(Horseman, horsemanPositionX, horsemanPositionY));
        }

        /// <summary>
        ///     Проверить перемещение всадника на пустой карте.
        /// </summary>
        [Test]
        [TestCase(10, 9, false)]
        [TestCase(9, 10, false)]
        [TestCase(30, 31, false)]
        [TestCase(31, 30, false)]
        [TestCase(20, 20, false)]
        [TestCase(10, 10, true)]
        [TestCase(30, 30, true)]
        [TestCase(11, 15, true)]
        [TestCase(25, 12, true)]
        public void CanMoveUnit_HorsemanOnEmptyMap(int X, int Y, bool CanMove)
        {
            const int startPositionX = 20;
            const int startPositionY = 20;

            Player Player = new Player(1, "Игрок №1", null);
            Horseman Horseman = new Horseman(Player, startPositionX, startPositionY);
            Map Map = CreateMap(Units: new[] {Horseman});
            GameController GameController = new GameController(Map);

            Assert.AreEqual(CanMove, GameController.CanMoveUnit(Horseman, X, Y));
        }

        /// <summary>
        ///     Проверить перемещение мечника на пустой карте.
        /// </summary>
        [Test]
        [TestCase(4, 5, false)]
        [TestCase(5, 4, false)]
        [TestCase(15, 16, false)]
        [TestCase(16, 15, false)]
        [TestCase(10, 10, false)]
        [TestCase(5, 5, true)]
        [TestCase(15, 15, true)]
        [TestCase(9, 10, true)]
        [TestCase(12, 7, true)]
        public void CanMoveUnit_SwordsmanOnEmptyMap(int X, int Y, bool CanMove)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player Player = new Player(1, "Игрок №1", null);
            Swordsman Swordsman = new Swordsman(Player, startPositionX, startPositionY);
            Map Map = CreateMap(Units: new[] {Swordsman});
            GameController GameController = new GameController(Map);

            Assert.AreEqual(CanMove, GameController.CanMoveUnit(Swordsman, X, Y));
        }

        /// <summary>
        ///     Проверить, что юнит может переместиться на клетку с травой.
        /// </summary>
        [Test]
        public void CanMoveUnit_SwordsmanOnGrass_True()
        {
            const int grassPositionX = 15;
            const int grassPositionY = 15;

            Player Player = new Player(1, "Игрок №1", null);
            Swordsman Swordsman = new Swordsman(Player, 10, 10);
            Grass Grass = new Grass(grassPositionX, grassPositionY);
            Map Map = CreateMap(new[] {Grass}, new[] {Swordsman});
            GameController GameController = new GameController(Map);

            Assert.True(GameController.CanMoveUnit(Swordsman, grassPositionX, grassPositionY));
        }

        /// <summary>
        ///     Проверить, что юнит может переместиться на клетку с травой.
        /// </summary>
        [Test]
        public void CanMoveUnit_SwordsmanOnWater_False()
        {
            const int waterPositionX = 15;
            const int waterPositionY = 15;

            Player Player = new Player(1, "Игрок №1", null);
            Swordsman Swordsman = new Swordsman(Player, 10, 10);
            Water Water = new Water(waterPositionX, waterPositionY);
            Map Map = CreateMap(new[] {Water}, new[] {Swordsman});
            GameController GameController = new GameController(Map);

            Assert.False(GameController.CanMoveUnit(Swordsman, waterPositionX, waterPositionY));
        }

        /// <summary>
        ///     Проверить корректность получения координат объекта.
        /// </summary>
        [Test]
        public void GetObjectCoordinates_AllTypes()
        {
            Player Player = new Player(1, "Игрок №1", null);
            Map Map = new Map(null, null);
            GameController GameController = new GameController(Map);


            Archer Archer = new Archer(Player, 1, 2);
            Coordinates ArcherCoordinates = GameController.GetObjectCoordinates(Archer);
            Assert.AreEqual(1, ArcherCoordinates.X);
            Assert.AreEqual(2, ArcherCoordinates.Y);

            Catapult Catapult = new Catapult(Player, 3, 4);
            Coordinates CatapultCoordinates = GameController.GetObjectCoordinates(Catapult);
            Assert.AreEqual(3, CatapultCoordinates.X);
            Assert.AreEqual(4, CatapultCoordinates.Y);

            Horseman Horseman = new Horseman(Player, 5, 6);
            Coordinates HorsemanCoordinates = GameController.GetObjectCoordinates(Horseman);
            Assert.AreEqual(5, HorsemanCoordinates.X);
            Assert.AreEqual(6, HorsemanCoordinates.Y);

            Swordsman Swordsman = new Swordsman(Player, 7, 8);
            Coordinates SwordsmanCoordinates = GameController.GetObjectCoordinates(Swordsman);
            Assert.AreEqual(7, SwordsmanCoordinates.X);
            Assert.AreEqual(8, SwordsmanCoordinates.Y);


            Grass Grass = new Grass(9, 10);
            Coordinates GrassCoordinates = GameController.GetObjectCoordinates(Grass);
            Assert.AreEqual(9, GrassCoordinates.X);
            Assert.AreEqual(10, GrassCoordinates.Y);

            Water Water = new Water(11, 12);
            Coordinates WaterCoordinates = GameController.GetObjectCoordinates(Water);
            Assert.AreEqual(11, WaterCoordinates.X);
            Assert.AreEqual(12, WaterCoordinates.Y);
        }

        /// <summary>
        ///     Проверить корректность получения изображения юнита.
        /// </summary>
        [Test]
        public void GetObjectSource_AllTypes()
        {
            Player Player = new Player(1, "Игрок №1", null);
            Map Map = CreateMap();
            GameController GameController = new GameController(Map);

            Assert.NotNull(GameController.GetObjectSource(new Archer(Player)));
            Assert.NotNull(GameController.GetObjectSource(new Catapult(Player)));
            Assert.NotNull(GameController.GetObjectSource(new Horseman(Player)));
            Assert.NotNull(GameController.GetObjectSource(new Swordsman(Player)));

            Assert.NotNull(GameController.GetObjectSource(new Grass()));
            Assert.NotNull(GameController.GetObjectSource(new Water()));
        }

        /// <summary>
        ///     Проверить корректность получения координат объекта.
        /// </summary>
        [Test]
        public void MoveUnit_AllTypes()
        {
            const int movePositionX = 15;
            const int movePositionY = 15;

            Player Player = new Player(1, "Игрок №1", null);
            Map Map = CreateMap();
            GameController GameController = new GameController(Map);


            Archer Archer = new Archer(Player, 14, 14);
            GameController.MoveUnit(Archer, movePositionX, movePositionY);
            Assert.AreEqual(movePositionX, Archer.UnitCoordinates.X);
            Assert.AreEqual(movePositionY, Archer.UnitCoordinates.Y);

            Catapult Catapult = new Catapult(Player, 14, 14);
            GameController.MoveUnit(Catapult, movePositionX, movePositionY);
            Assert.AreEqual(movePositionX, Catapult.UnitCoordinates.X);
            Assert.AreEqual(movePositionY, Catapult.UnitCoordinates.Y);

            Horseman Horseman = new Horseman(Player, 14, 14);
            GameController.MoveUnit(Horseman, movePositionX, movePositionY);
            Assert.AreEqual(movePositionX, Horseman.UnitCoordinates.X);
            Assert.AreEqual(movePositionY, Horseman.UnitCoordinates.Y);

            Swordsman Swordsman = new Swordsman(Player, 14, 14);
            GameController.MoveUnit(Swordsman, movePositionX, movePositionY);
            Assert.AreEqual(movePositionX, Swordsman.UnitCoordinates.X);
            Assert.AreEqual(movePositionY, Swordsman.UnitCoordinates.Y);
        }
    }
}