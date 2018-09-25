using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using GameStore.WebUI.HtmlHelpers;

namespace GameStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
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

            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            // act
            GameListViewModel result = (GameListViewModel)controller.List(null, 2).Model;

            // assert
            List<Game> games = result.Games.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].Name, "Game_4");
            Assert.AreEqual(games[1].Name, "Game_5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // assertion
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                            + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                            + @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();

            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game_1" },
                new Game { GameId = 2, Name = "Game_2" },
                new Game { GameId = 3, Name = "Game_3" },
                new Game { GameId = 4, Name = "Game_4" },
                new Game { GameId = 5, Name = "Game_5" }
            });

            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            // act
            GameListViewModel result = (GameListViewModel)controller.List(null, 2).Model;

            // Assers
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Games()
        {
            // arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();

            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game_1", Category = "Cat_1"},
                new Game { GameId = 2, Name = "Game_2", Category = "Cat_2"},
                new Game { GameId = 3, Name = "Game_3", Category = "Cat_1"},
                new Game { GameId = 4, Name = "Game_4", Category = "Cat_2"},
                new Game { GameId = 5, Name = "Game_5", Category = "Cat_3"}
            });

            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            // action
            List<Game> result = ((GameListViewModel)controller.List("Cat_2", 1).Model).Games.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Game_2" && result[0].Category == "Cat_2");
            Assert.IsTrue(result[1].Name == "Game_4" && result[1].Category == "Cat_2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // arrange - имитированное хранилище
            Mock<IGameRepository> mock = new Mock<IGameRepository>();

            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game_1", Category = "Симулятор" },
                new Game { GameId = 2, Name = "Game_2", Category = "Симулятор" },
                new Game { GameId = 3, Name = "Game_3", Category = "Шутер" },
                new Game { GameId = 4, Name = "Game_4", Category = "RPG" }
            });

            // arrange - создание контроллера
            NavController target = new NavController(mock.Object);

            // act
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // assert
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "RPG");
            Assert.AreEqual(results[1], "Симулятор");
            Assert.AreEqual(results[2], "Шутер");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // arrange - создание имитированного хранилища
            Mock<IGameRepository> mock = new Mock<IGameRepository>();

            mock.Setup(m => m.Games).Returns(new Game[]
            {
                new Game { GameId = 1, Name = "Game_1", Category = "Симулятор" },
                new Game { GameId = 2, Name = "Game_2", Category = "Шутер" }
            });

            // arrange - создание контроллера
            NavController target = new NavController(mock.Object);

            // arrange - определение выюранной категории 
            string categoryToSelect = "Шутер";

            // act
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_specific_Game_Count()
        {
            // arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();

            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game_1", Category = "Cat_1" },
                new Game { GameId = 2, Name = "Game_2", Category = "Cat_2" },
                new Game { GameId = 3, Name = "Game_3", Category = "Cat_1" },
                new Game { GameId = 4, Name = "Game_4", Category = "Cat_2" },
                new Game { GameId = 5, Name = "Game_5", Category = "Cat_3" }
            });

            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            // act
            int res1 = ((GameListViewModel)controller.List("Cat_1").Model).PagingInfo.TotalItems;
            int res2 = ((GameListViewModel)controller.List("Cat_2").Model).PagingInfo.TotalItems;
            int res3 = ((GameListViewModel)controller.List("Cat_3").Model).PagingInfo.TotalItems;
            int resAll = ((GameListViewModel)controller.List("null").Model).PagingInfo.TotalItems;

            // assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
