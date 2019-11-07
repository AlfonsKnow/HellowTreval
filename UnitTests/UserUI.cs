using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using System.Collections.Generic;
using Domain.Entities;
using WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.HtmlHelpers;

namespace UnitTests
{
    [TestClass]
    public class UserUI
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<ITourRepository> mock = new Mock<ITourRepository>();
            mock.Setup(m => m.GetAllTours()).Returns(new List<Tour>
            {
                new Tour{TourId = 1, Name = "Tour1"},
                new Tour{TourId = 2, Name = "Tour2"},
                new Tour{TourId = 3, Name = "Tour3"},
                new Tour{TourId = 4, Name = "Tour4"},
                new Tour{TourId = 5, Name = "Tour5"}
            });

            ToursController controller = new ToursController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            ToursListViewModel result = (ToursListViewModel)controller.List(null, 2).Model;

            // Утверждение (assert)
            List<Tour> tours = result.Tours.ToList();
            Assert.IsTrue(tours.Count == 2);
            Assert.AreEqual(tours[0].Name, "Tour4");
            Assert.AreEqual(tours[1].Name, "Tour5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Организация
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-light"" href=""page1"">1</a>"
                + @"<a class=""btn btn-primary selected"" href=""page2"">2</a>"
                + @"<a class=""btn btn-light"" href=""page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<ITourRepository> mock = new Mock<ITourRepository>();
            mock.Setup(m => m.GetAllTours()).Returns(new List<Tour>
            {
                new Tour{TourId = 1, Name = "Tour1"},
                new Tour{TourId = 2, Name = "Tour2"},
                new Tour{TourId = 3, Name = "Tour3"},
                new Tour{TourId = 4, Name = "Tour4"},
                new Tour{TourId = 5, Name = "Tour5"}
            });

            ToursController controller = new ToursController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            ToursListViewModel result = (ToursListViewModel)controller.List(null, 2).Model;

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Tours()
        {
            // Организация (arrange)
            Mock<ITourRepository> mock = new Mock<ITourRepository>();
            mock.Setup(m => m.GetAllTours()).Returns(new List<Tour>
            {
                new Tour{TourId = 1, Name = "Tour1", Type="Type1"},
                new Tour{TourId = 2, Name = "Tour2", Type="Type2"},
                new Tour{TourId = 3, Name = "Tour3", Type="Type1"},
                new Tour{TourId = 4, Name = "Tour4", Type="Type3"},
                new Tour{TourId = 5, Name = "Tour5", Type="Type2"}
            });

            ToursController controller = new ToursController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            List<Tour> result = ((ToursListViewModel)controller.List("Type2", 1).Model).Tours.ToList();

            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Tour2" && result[0].Type == "Type2");
            Assert.IsTrue(result[1].Name == "Tour5" && result[1].Type == "Type2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация (arrange)
            Mock<ITourRepository> mock = new Mock<ITourRepository>();
            mock.Setup(m => m.GetAllTours()).Returns(new List<Tour>
            {
                new Tour{TourId = 1, Name = "Tour1", Type="Type1"},
                new Tour{TourId = 2, Name = "Tour2", Type="Type2"},
                new Tour{TourId = 3, Name = "Tour3", Type="Type1"},
                new Tour{TourId = 4, Name = "Tour4", Type="Type3"},
                new Tour{TourId = 5, Name = "Tour5", Type="Type2"}
            });

            NavController target = new NavController(mock.Object);

            // Действие (act)
            List<string> result = ((IEnumerable<string>)target.Menu().Model).ToList();

            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "Type1");
            Assert.AreEqual(result[1], "Type2");
            Assert.AreEqual(result[2], "Type3");
        }

        [TestMethod]
        public void Indicates_Selected_Type()
        {
            // Организация (arrange)
            Mock<ITourRepository> mock = new Mock<ITourRepository>();
            mock.Setup(m => m.GetAllTours()).Returns(new List<Tour>
            {
                new Tour{TourId = 1, Name = "Tour1", Type="Type1"},
                new Tour{TourId = 2, Name = "Tour2", Type="Type2"},
                new Tour{TourId = 3, Name = "Tour3", Type="Type1"},
                new Tour{TourId = 4, Name = "Tour4", Type="Type3"},
                new Tour{TourId = 5, Name = "Tour5", Type="Type2"}
            });

            NavController target = new NavController(mock.Object);

            string genreToSelect = "Type2";

            // Действие (act)
            string result = target.Menu(genreToSelect).ViewBag.SelectedType;

            Assert.AreEqual(genreToSelect, result);
        }
    }
}
