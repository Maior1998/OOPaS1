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
        /// <param name="gameController">Контроллер игры.</param>
        /// <param name="attackerUnit">Юнит, который наносит удар.</param>
        /// <param name="targetUnit">Юнит, который является целью.</param>
        /// <returns>Количество ударов, которое было нанесено юниту.</returns>
        /// <remarks>
        ///     Проверка не точная. Считается какое количество ударов нужно, чтобы убить противника.
        ///     Смерть считается по тому, что больше нельзя атаковать. В общем случае, такая проверка работоспособна.
        /// </remarks>
        private static int GetAttacksCount(GameController gameController, object attackerUnit, object targetUnit)
        {
            int count = 0;
            while (gameController.CanAttackUnit(attackerUnit, targetUnit))
            {
                gameController.AttackUnit(attackerUnit, targetUnit);
                count++;
            }

            return count;
        }

        /// <summary>
        ///     Создать карту.
        /// </summary>
        /// <param name="ground">Информация о местности.</param>
        /// <param name="units">Список юнитов.</param>
        private static Map CreateMap(IReadOnlyList<object> ground = null, IReadOnlyList<object> units = null)
        {
            return new Map(ground ?? new object[0], units ?? new object[0]);
        }

        /// <summary>
        ///     Проверить дальнюю атаку лучника.
        /// </summary>
        [Test]
        public void AttackUnit_ArcherAttackAllTypes()
        {
            Player player1 = new Player(1, "Игрок №1", null);
            Player player2 = new Player(2, "Игрок №2", null);
            Archer archer = new Archer(player1, 8, 8);
            Map map = CreateMap();
            GameController gameController = new GameController(map);


            // Лучник имеет 50 жизней. Погибнет за один удар.
            Archer archerTarget = new Archer(player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(gameController, archer, archerTarget));

            // Катапульта имеет 75 жизней. Погибнет за два удара.
            Catapult catapultTarget = new Catapult(player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(gameController, archer, catapultTarget));

            // Всадник имеет 200 жизней. Необходимо 4 удара.
            Horseman horsemanTarget = new Horseman(player2, 10, 10);
            Assert.AreEqual(4, GetAttacksCount(gameController, archer, horsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за два удара.
            Swordsman swordsmanTarget = new Swordsman(player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(gameController, archer, swordsmanTarget));
        }

        /// <summary>
        ///     Проверить ближнюю атаку лучника.
        /// </summary>
        [Test]
        public void AttackUnit_ArcherAttackCloseCombatAllTypes()
        {
            Player player1 = new Player(1, "Игрок №1", null);
            Player player2 = new Player(2, "Игрок №2", null);
            Archer archer = new Archer(player1, 9, 9);
            Map map = CreateMap();
            GameController gameController = new GameController(map);


            // Лучник имеет 50 жизней. Погибнет за два удара.
            Archer archerTarget = new Archer(player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(gameController, archer, archerTarget));

            // Катапульта имеет 75 жизней. Погибнет за три удара.
            Catapult catapultTarget = new Catapult(player2, 10, 10);
            Assert.AreEqual(3, GetAttacksCount(gameController, archer, catapultTarget));

            // Всадник имеет 200 жизней. Необходимо 8 ударов.
            Horseman horsemanTarget = new Horseman(player2, 10, 10);
            Assert.AreEqual(8, GetAttacksCount(gameController, archer, horsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за 4 удара.
            Swordsman swordsmanTarget = new Swordsman(player2, 10, 10);
            Assert.AreEqual(4, GetAttacksCount(gameController, archer, swordsmanTarget));
        }

        /// <summary>
        ///     Проверить дальнюю атаку катапульты.
        /// </summary>
        [Test]
        public void AttackUnit_CatapultAttackAllTypes()
        {
            Player player1 = new Player(1, "Игрок №1", null);
            Player player2 = new Player(2, "Игрок №2", null);
            Catapult catapult = new Catapult(player1, 8, 8);
            Map map = CreateMap();
            GameController gameController = new GameController(map);


            // Лучник имеет 50 жизней. Погибнет за один удар.
            Archer archerTarget = new Archer(player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(gameController, catapult, archerTarget));

            // Катапульта имеет 75 жизней. Погибнет за один удар.
            Catapult catapultTarget = new Catapult(player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(gameController, catapult, catapultTarget));

            // Всадник имеет 200 жизней. Необходимо 2 удара.
            Horseman horsemanTarget = new Horseman(player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(gameController, catapult, horsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за один удар.
            Swordsman swordsmanTarget = new Swordsman(player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(gameController, catapult, swordsmanTarget));
        }

        /// <summary>
        ///     Проверить ближнюю атаку катапульты.
        /// </summary>
        [Test]
        public void AttackUnit_CatapultAttackCloseCombatAllTypes()
        {
            Player player1 = new Player(1, "Игрок №1", null);
            Player player2 = new Player(2, "Игрок №2", null);
            Catapult catapult = new Catapult(player1, 9, 9);
            Map map = CreateMap();
            GameController gameController = new GameController(map);


            // Лучник имеет 50 жизней. Погибнет за один удар.
            Archer archerTarget = new Archer(player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(gameController, catapult, archerTarget));

            // Катапульта имеет 75 жизней. Погибнет за два удара.
            Catapult catapultTarget = new Catapult(player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(gameController, catapult, catapultTarget));

            // Всадник имеет 200 жизней. Необходимо 4 удара.
            Horseman horsemanTarget = new Horseman(player2, 10, 10);
            Assert.AreEqual(4, GetAttacksCount(gameController, catapult, horsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за два удара.
            Swordsman swordsmanTarget = new Swordsman(player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(gameController, catapult, swordsmanTarget));
        }

        /// <summary>
        ///     Проверить атаку всадника.
        /// </summary>
        [Test]
        public void AttackUnit_HorsemanAttackAllTypes()
        {
            Player player1 = new Player(1, "Игрок №1", null);
            Player player2 = new Player(2, "Игрок №2", null);
            Horseman horseman = new Horseman(player1, 9, 9);
            Map map = CreateMap();
            GameController gameController = new GameController(map);


            // Лучник имеет 50 жизней. Погибнет за один удар.
            Archer archerTarget = new Archer(player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(gameController, horseman, archerTarget));

            // Катапульта имеет 75 жизней. Погибнет за один удар.
            Catapult catapultTarget = new Catapult(player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(gameController, horseman, catapultTarget));

            // Всадник имеет 200 жизней. Необходимо 3 удара.
            Horseman horsemanTarget = new Horseman(player2, 10, 10);
            Assert.AreEqual(3, GetAttacksCount(gameController, horseman, horsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за два удара.
            Swordsman swordsmanTarget = new Swordsman(player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(gameController, horseman, swordsmanTarget));
        }

        /// <summary>
        ///     Проверить атаку мечника.
        /// </summary>
        [Test]
        public void AttackUnit_SwordsmanAttackAllTypes()
        {
            Player player1 = new Player(1, "Игрок №1", null);
            Player player2 = new Player(2, "Игрок №2", null);
            Swordsman swordsman = new Swordsman(player1, 9, 9);
            Map map = CreateMap();
            GameController gameController = new GameController(map);


            // Лучник имеет 50 жизней. Погибнет за один удар.
            Archer archerTarget = new Archer(player2, 10, 10);
            Assert.AreEqual(1, GetAttacksCount(gameController, swordsman, archerTarget));

            // Катапульта имеет 75 жизней. Погибнет за два удара.
            Catapult catapultTarget = new Catapult(player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(gameController, swordsman, catapultTarget));

            // Всадник имеет 200 жизней. Необходимо 4 удара.
            Horseman horsemanTarget = new Horseman(player2, 10, 10);
            Assert.AreEqual(4, GetAttacksCount(gameController, swordsman, horsemanTarget));

            // Мечник имеет 100 жизней. Погибнет за два удара.
            Swordsman swordsmanTarget = new Swordsman(player2, 10, 10);
            Assert.AreEqual(2, GetAttacksCount(gameController, swordsman, swordsmanTarget));
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
        public void CanAttackUnit_Archer(int x, int y, bool canAttack)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player player1 = new Player(1, "Игрок №1", null);
            Player player2 = new Player(2, "Игрок №2", null);
            Archer archer = new Archer(player1, startPositionX, startPositionY);
            Archer target = new Archer(player2, x, y);
            Map map = CreateMap(units: new object[] {archer, target});
            GameController gameController = new GameController(map);

            Assert.AreEqual(canAttack, gameController.CanAttackUnit(archer, target));
        }

        /// <summary>
        ///     Проверить, что невозможна атака дружественного юнита.
        /// </summary>
        [Test]
        public void CanAttackUnit_ArcherAttackFriend_False()
        {
            Player player = new Player(1, "Игрок №1", null);
            Archer archer = new Archer(player, 10, 10);
            Archer target = new Archer(player, 11, 11);
            Map map = CreateMap(units: new[] {archer, target});
            GameController gameController = new GameController(map);

            Assert.False(gameController.CanAttackUnit(archer, target));
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
        public void CanAttackUnit_Catapult(int x, int y, bool canAttack)
        {
            const int startPositionX = 20;
            const int startPositionY = 20;

            Player player1 = new Player(1, "Игрок №1", null);
            Player player2 = new Player(2, "Игрок №2", null);
            Catapult catapult = new Catapult(player1, startPositionX, startPositionY);
            Archer target = new Archer(player2, x, y);
            Map map = CreateMap(units: new object[] {catapult, target});
            GameController gameController = new GameController(map);

            Assert.AreEqual(canAttack, gameController.CanAttackUnit(catapult, target));
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
        public void CanAttackUnit_Horseman(int x, int y, bool canAttack)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player player1 = new Player(1, "Игрок №1", null);
            Player player2 = new Player(2, "Игрок №2", null);
            Horseman horseman = new Horseman(player1, startPositionX, startPositionY);
            Archer target = new Archer(player2, x, y);
            Map map = CreateMap(units: new object[] {horseman, target});
            GameController gameController = new GameController(map);

            Assert.AreEqual(canAttack, gameController.CanAttackUnit(horseman, target));
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
        public void CanAttackUnit_Swordsman(int x, int y, bool canAttack)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player player1 = new Player(1, "Игрок №1", null);
            Player player2 = new Player(2, "Игрок №2", null);
            Swordsman swordsman = new Swordsman(player1, startPositionX, startPositionY);
            Archer target = new Archer(player2, x, y);
            Map map = CreateMap(units: new object[] {swordsman, target});
            GameController gameController = new GameController(map);

            Assert.AreEqual(canAttack, gameController.CanAttackUnit(swordsman, target));
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
        public void CanMoveUnit_ArcherOnEmptyMap(int x, int y, bool canMove)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player player = new Player(1, "Игрок №1", null);
            Archer archer = new Archer(player, startPositionX, startPositionY);
            Map map = CreateMap(units: new[] {archer});
            GameController gameController = new GameController(map);

            Assert.AreEqual(canMove, gameController.CanMoveUnit(archer, x, y));
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
        public void CanMoveUnit_CatapultOnEmptyMap(int x, int y, bool canMove)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player player = new Player(1, "Игрок №1", null);
            Catapult catapult = new Catapult(player, startPositionX, startPositionY);
            Map map = CreateMap(units: new[] {catapult});
            GameController gameController = new GameController(map);

            Assert.AreEqual(canMove, gameController.CanMoveUnit(catapult, x, y));
        }

        /// <summary>
        ///     Проверить, что юнит не может переместиться на клетку, которую занял другой юнит.
        /// </summary>
        [Test]
        public void CanMoveUnit_CatapultOnHorseman_False()
        {
            const int horsemanPositionX = 15;
            const int horsemanPositionY = 15;

            Player player = new Player(1, "Игрок №1", null);
            Catapult catapult = new Catapult(player, 10, 10);
            Horseman horseman = new Horseman(player, horsemanPositionX, horsemanPositionY);
            Map map = CreateMap(units: new object[] {catapult, horseman});
            GameController gameController = new GameController(map);

            Assert.False(gameController.CanMoveUnit(horseman, horsemanPositionX, horsemanPositionY));
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
        public void CanMoveUnit_HorsemanOnEmptyMap(int x, int y, bool canMove)
        {
            const int startPositionX = 20;
            const int startPositionY = 20;

            Player player = new Player(1, "Игрок №1", null);
            Horseman horseman = new Horseman(player, startPositionX, startPositionY);
            Map map = CreateMap(units: new[] {horseman});
            GameController gameController = new GameController(map);

            Assert.AreEqual(canMove, gameController.CanMoveUnit(horseman, x, y));
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
        public void CanMoveUnit_SwordsmanOnEmptyMap(int x, int y, bool canMove)
        {
            const int startPositionX = 10;
            const int startPositionY = 10;

            Player player = new Player(1, "Игрок №1", null);
            Swordsman swordsman = new Swordsman(player, startPositionX, startPositionY);
            Map map = CreateMap(units: new[] {swordsman});
            GameController gameController = new GameController(map);

            Assert.AreEqual(canMove, gameController.CanMoveUnit(swordsman, x, y));
        }

        /// <summary>
        ///     Проверить, что юнит может переместиться на клетку с травой.
        /// </summary>
        [Test]
        public void CanMoveUnit_SwordsmanOnGrass_True()
        {
            const int grassPositionX = 15;
            const int grassPositionY = 15;

            Player player = new Player(1, "Игрок №1", null);
            Swordsman swordsman = new Swordsman(player, 10, 10);
            Grass grass = new Grass(grassPositionX, grassPositionY);
            Map map = CreateMap(new[] {grass}, new[] {swordsman});
            GameController gameController = new GameController(map);

            Assert.True(gameController.CanMoveUnit(swordsman, grassPositionX, grassPositionY));
        }

        //TODO: да епт, это трава или вода, че происходит вообще
        /// <summary>
        ///     Проверить, что юнит может переместиться на клетку с травой.
        /// </summary>
        [Test]
        public void CanMoveUnit_SwordsmanOnWater_False()
        {
            const int waterPositionX = 15;
            const int waterPositionY = 15;

            Player player = new Player(1, "Игрок №1", null);
            Swordsman swordsman = new Swordsman(player, 10, 10);
            Water water = new Water(waterPositionX, waterPositionY);
            Map map = CreateMap(new[] {water}, new[] {swordsman});
            GameController gameController = new GameController(map);

            Assert.False(gameController.CanMoveUnit(swordsman, waterPositionX, waterPositionY));
        }

        /// <summary>
        ///     Проверить корректность получения координат объекта.
        /// </summary>
        [Test]
        public void GetObjectCoordinates_AllTypes()
        {
            Player player = new Player(1, "Игрок №1", null);
            Map map = new Map(null, null);
            GameController gameController = new GameController(map);


            Archer archer = new Archer(player, 1, 2);
            Coordinates archerCoordinates = gameController.GetObjectCoordinates(archer);
            Assert.AreEqual(1, archerCoordinates.X);
            Assert.AreEqual(2, archerCoordinates.Y);

            Catapult catapult = new Catapult(player, 3, 4);
            Coordinates catapultCoordinates = gameController.GetObjectCoordinates(catapult);
            Assert.AreEqual(3, catapultCoordinates.X);
            Assert.AreEqual(4, catapultCoordinates.Y);

            Horseman horseman = new Horseman(player, 5, 6);
            Coordinates horsemanCoordinates = gameController.GetObjectCoordinates(horseman);
            Assert.AreEqual(5, horsemanCoordinates.X);
            Assert.AreEqual(6, horsemanCoordinates.Y);

            Swordsman swordsman = new Swordsman(player, 7, 8);
            Coordinates swordsmanCoordinates = gameController.GetObjectCoordinates(swordsman);
            Assert.AreEqual(7, swordsmanCoordinates.X);
            Assert.AreEqual(8, swordsmanCoordinates.Y);


            Grass grass = new Grass(9, 10);
            Coordinates grassCoordinates = gameController.GetObjectCoordinates(grass);
            Assert.AreEqual(9, grassCoordinates.X);
            Assert.AreEqual(10, grassCoordinates.Y);

            Water water = new Water(11, 12);
            Coordinates waterCoordinates = gameController.GetObjectCoordinates(water);
            Assert.AreEqual(11, waterCoordinates.X);
            Assert.AreEqual(12, waterCoordinates.Y);
        }

        /// <summary>
        ///     Проверить корректность получения изображения юнита.
        /// </summary>
        [Test]
        public void GetObjectSource_AllTypes()
        {
            Player player = new Player(1, "Игрок №1", null);
            Map map = CreateMap();
            GameController gameController = new GameController(map);

            Assert.NotNull(gameController.GetObjectSource(new Archer(player)));
            Assert.NotNull(gameController.GetObjectSource(new Catapult(player)));
            Assert.NotNull(gameController.GetObjectSource(new Horseman(player)));
            Assert.NotNull(gameController.GetObjectSource(new Swordsman(player)));

            Assert.NotNull(gameController.GetObjectSource(new Grass()));
            Assert.NotNull(gameController.GetObjectSource(new Water()));
        }

        /// <summary>
        ///     Проверить корректность получения координат объекта.
        /// </summary>
        [Test]
        public void MoveUnit_AllTypes()
        {
            const int movePositionX = 15;
            const int movePositionY = 15;

            Player player = new Player(1, "Игрок №1", null);
            Map map = CreateMap();
            GameController gameController = new GameController(map);


            Archer archer = new Archer(player, 14, 14);
            gameController.MoveUnit(archer, movePositionX, movePositionY);
            Assert.AreEqual(movePositionX, archer.UnitCoordinates.X);
            Assert.AreEqual(movePositionY, archer.UnitCoordinates.Y);

            Catapult catapult = new Catapult(player, 14, 14);
            gameController.MoveUnit(catapult, movePositionX, movePositionY);
            Assert.AreEqual(movePositionX, catapult.UnitCoordinates.X);
            Assert.AreEqual(movePositionY, catapult.UnitCoordinates.Y);

            Horseman horseman = new Horseman(player, 14, 14);
            gameController.MoveUnit(horseman, movePositionX, movePositionY);
            Assert.AreEqual(movePositionX, horseman.UnitCoordinates.X);
            Assert.AreEqual(movePositionY, horseman.UnitCoordinates.Y);

            Swordsman swordsman = new Swordsman(player, 14, 14);
            gameController.MoveUnit(swordsman, movePositionX, movePositionY);
            Assert.AreEqual(movePositionX, swordsman.UnitCoordinates.X);
            Assert.AreEqual(movePositionY, swordsman.UnitCoordinates.Y);
        }
    }
}