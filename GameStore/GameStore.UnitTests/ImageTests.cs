using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // arrange
            Game game = new Game { GameId = 2, Name = "Game_2", ImageData = new byte[] { }, ImageMimeType = "image/png" };

            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game_1" },
                game,
                new Game { GameId = 3, Name = "Game_3" }
            }.AsQueryable());

            GameController controller = new GameController(mock.Object);

            // act
            ActionResult result = controller.GetImage(2);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(game.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // arrange
            Game game = new Game { GameId = 2, Name = "Game_2", ImageData = new byte[] { }, ImageMimeType = "image/png" };

            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game_1" },
                new Game { GameId = 2, Name = "Game_2" }
            }.AsQueryable());

            GameController controller = new GameController(mock.Object);

            // act
            ActionResult result = controller.GetImage(10);

            // assert
            Assert.IsNull(result);
        }
    }
}
