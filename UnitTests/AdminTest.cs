using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Controllers;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void List_Contains_All_Formats()
        {
            // Организация (arrange)
            Mock<IFormatRepository> mock = new Mock<IFormatRepository>();
            mock.Setup(m => m.GetAllFormats()).Returns(new List<Format>
            {
                new Format{FormatId = 1, FormatName = "Format1"},
                new Format{FormatId = 2, FormatName = "Format2"},
                new Format{FormatId = 3, FormatName = "Format3"},
                new Format{FormatId = 4, FormatName = "Format4"},
                new Format{FormatId = 5, FormatName = "Format5"}
            });

            AdminFormatController controller = new AdminFormatController(mock.Object);

            // Действие (act)
            FormatListViewModel result = (FormatListViewModel)controller.List(null).Model;
            List<Format> formats = result.Formats.ToList();

            //Утверждение(assert)
            Assert.AreEqual(formats.Count(), 5);
            Assert.AreEqual(formats[0].FormatName, "Format1");
            Assert.AreEqual(formats[1].FormatName, "Format2");
        }

        [TestMethod]
        public void List_Find_By_Query()
        {
            // Организация (arrange)
            Mock<IFormatRepository> mock = new Mock<IFormatRepository>();
            mock.Setup(m => m.GetAllFormats()).Returns(new List<Format>
            {
                new Format{FormatId = 1, FormatName = "Format1"},
                new Format{FormatId = 2, FormatName = "Query"},
                new Format{FormatId = 3, FormatName = "Query"},
                new Format{FormatId = 4, FormatName = "Query"},
                new Format{FormatId = 5, FormatName = "Format5"}
            });

            AdminFormatController controller = new AdminFormatController(mock.Object);

            // Действие (act)
            FormatListViewModel result = (FormatListViewModel)controller.List("Query").Model;
            List<Format> formats = result.Formats.ToList();

            //Утверждение(assert)
            Assert.AreEqual(formats.Count(), 3);
            Assert.AreEqual(formats[1].FormatName, "Query");
            Assert.AreEqual(formats[2].FormatName, "Query");
            Assert.AreEqual(formats[1].FormatName, "Query");
        }

        [TestMethod]
        public void Can_Edit_Format()
        {
            // Организация (arrange)
            Mock<IFormatRepository> mock = new Mock<IFormatRepository>();
            mock.Setup(m => m.GetAllFormats()).Returns(new List<Format>
            {
                new Format{FormatId = 1, FormatName = "Format1"},
                new Format{FormatId = 2, FormatName = "Format2"},
                new Format{FormatId = 3, FormatName = "Format3"},
                new Format{FormatId = 4, FormatName = "Format4"},
                new Format{FormatId = 5, FormatName = "Format5"}
            });

            AdminFormatController controller = new AdminFormatController(mock.Object);

            // Действие (act)
            Format format1 = controller.Edit(1).ViewData.Model as Format;
            Format format2 = controller.Edit(2).ViewData.Model as Format;
            Format format3 = controller.Edit(3).ViewData.Model as Format;

            // Утверждение (assert)
            Assert.AreEqual(1, format1.FormatId);
            Assert.AreEqual(2, format2.FormatId);
            Assert.AreEqual(3, format3.FormatId);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            Mock<IFormatRepository> mock = new Mock<IFormatRepository>();
            AdminFormatController controller = new AdminFormatController(mock.Object);

            Format format = new Format { FormatName = "Test" };

            ActionResult result = controller.Edit(format);

            mock.Verify(m => m.SaveFormat(format));

            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Save_Invalid_Changes()
        {
            Mock<IFormatRepository> mock = new Mock<IFormatRepository>();
            AdminFormatController controller = new AdminFormatController(mock.Object);

            Format format = new Format { FormatName = "Test" };

            controller.ModelState.AddModelError("error", "error");

            ActionResult result = controller.Edit(format);

            mock.Verify(m => m.SaveFormat(It.IsAny<Format>()), Times.Never());

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
