using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Games()
        {
            // arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();

            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game{ GameId = 1, Name = "Game_1" },
                new Game{ GameId = 2, Name = "Game_2" },
                new Game{ GameId = 3, Name = "Game_3" },
                new Game{ GameId = 4, Name = "Game_4" },
                new Game{ GameId = 5, Name = "Game_5" }
            });

            AdminController controller = new AdminController(mock.Object);

            // act
            List<Game> result = ((IEnumerable<Game>)controller.Index().ViewData.Model).ToList();

            // assert
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Game_1", result[0].Name);
            Assert.AreEqual("Game_2", result[1].Name);
            Assert.AreEqual("Game_3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Game()
        {
            // arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();

            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game{ GameId = 1, Name = "Game_1" },
                new Game{ GameId = 2, Name = "Game_2" },
                new Game{ GameId = 3, Name = "Game_3" },
                new Game{ GameId = 4, Name = "Game_4" },
                new Game{ GameId = 5, Name = "Game_5" }
            });

            AdminController controller = new AdminController(mock.Object);

            // act
            Game game_1 = controller.Edit(1).ViewData.Model as Game;
            Game game_3 = controller.Edit(2).ViewData.Model as Game;
            Game game_2 = controller.Edit(3).ViewData.Model as Game;

            // assert
            Assert.AreEqual(1, game_1.GameId);
            Assert.AreEqual(2, game_2.GameId);
            Assert.AreEqual(3, game_3.GameId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Game()
        {
            // arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();

            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game{ GameId = 1, Name = "Game_1" },
                new Game{ GameId = 2, Name = "Game_2" },
                new Game{ GameId = 3, Name = "Game_3" },
                new Game{ GameId = 4, Name = "Game_4" },
                new Game{ GameId = 5, Name = "Game_5" }
            });

            AdminController controller = new AdminController(mock.Object);

            // act
            Game result = controller.Edit(6).ViewData.Model as Game;

            // assert
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();

            AdminController controller = new AdminController(mock.Object);

            Game game = new Game { Name = "Test" };

            // act
            ActionResult result = controller.Edit(game);

            // assert
            mock.Verify(m => m.SaveGame(game));                         // обращение к хранилищу
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));     // проверка типа результата метода
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();

            AdminController controller = new AdminController(mock.Object);

            Game game = new Game { Name = "Test" };

            controller.ModelState.AddModelError("error", "error");

            // act
            ActionResult result = controller.Edit(game);

            // assert
            mock.Verify(m => m.SaveGame(It.IsAny<Game>()), Times.Never());      // обращение к хранилищу не производится 
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Games()
        {
            // arrange
            Game game = new Game { GameId = 2, Name = "Game_2" };

            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game_1" },
                new Game { GameId = 2, Name = "Game_2" },
                new Game { GameId = 3, Name = "Game_3" },
                new Game { GameId = 4, Name = "Game_4" },
                new Game { GameId = 5, Name = "Game_5" }
            });

            AdminController controller = new AdminController(mock.Object);

            // act
            controller.Delete(game.GameId);

            // assert
            mock.Verify(m => m.DeleteGame(game.GameId));
        }
    }
}
