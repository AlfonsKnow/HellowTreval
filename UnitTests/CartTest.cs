using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Domain.Abstract;
using WebUI.Controllers;
using System.Web.Mvc;
using WebUI.Models;


namespace UnitTests
{
    [TestClass]
    public class CartTest
    {
        [TestClass]
        public class CartTests
        {
            [TestMethod]
            public void Can_Add_New_Lines()
            {
                // Организация
                Tour tour1 = new Tour { TourId = 1, Name = "Tour1" };
                Tour tour2 = new Tour { TourId = 2, Name = "Tour2" };

                Cart cart = new Cart();

                // Действие
                cart.AddItem(tour1, 1);
                cart.AddItem(tour2, 1);
                List<CartLine> results = cart.Lines.ToList();

                // Утвержение
                Assert.AreEqual(results.Count(), 2);
                Assert.AreEqual(results[0].Tour, tour1);
                Assert.AreEqual(results[1].Tour, tour2);
            }

            [TestMethod]
            public void Can_Add_Quantity_For_Existing_Lines()
            {
                // Организация
                Tour tour1 = new Tour { TourId = 1, Name = "Tour1" };
                Tour tour2 = new Tour { TourId = 2, Name = "Tour2" };

                Cart cart = new Cart();

                // Действие
                cart.AddItem(tour1, 1);
                cart.AddItem(tour2, 1);
                cart.AddItem(tour1, 5);
                List<CartLine> results = cart.Lines.OrderBy(c => c.Tour.TourId).ToList();

                // Утвержение
                Assert.AreEqual(results.Count(), 2);
                Assert.AreEqual(results[0].Quantity, 6);
                Assert.AreEqual(results[1].Quantity, 1);
            }

            [TestMethod]
            public void Can_Remove_Line()
            {
                // Организация
                Tour tour1 = new Tour { TourId = 1, Name = "Tour1" };
                Tour tour2 = new Tour { TourId = 2, Name = "Tour2" };
                Tour tour3 = new Tour { TourId = 3, Name = "Tour3" };

                Cart cart = new Cart();

                // Действие
                cart.AddItem(tour1, 1);
                cart.AddItem(tour2, 1);
                cart.AddItem(tour1, 5);
                cart.AddItem(tour3, 2);
                cart.RemoveLine(tour2);

                // Утвержение
                Assert.AreEqual(cart.Lines.Where(c => c.Tour == tour2).Count(), 0);
                Assert.AreEqual(cart.Lines.Count(), 2);
            }

            [TestMethod]
            public void Calculate_Cart_Total()
            {
                // Организация
                Tour tour1 = new Tour { TourId = 1, Name = "Tour1", Price = 100 };
                Tour tour2 = new Tour { TourId = 2, Name = "Tour2", Price = 55 };

                Cart cart = new Cart();

                // Действие
                cart.AddItem(tour1, 1);
                cart.AddItem(tour2, 1);
                cart.AddItem(tour1, 5);
                decimal result = cart.ComputeTotalValue();

                // Утвержение
                Assert.AreEqual(result, 655);
            }

            [TestMethod]
            public void Can_Clear_Contents()
            {
                // Организация
                Tour tour1 = new Tour { TourId = 1, Name = "Tour1", Price = 100 };
                Tour tour2 = new Tour { TourId = 2, Name = "Tour2", Price = 55 };

                Cart cart = new Cart();

                // Действие
                cart.AddItem(tour1, 1);
                cart.AddItem(tour2, 1);
                cart.AddItem(tour1, 5);
                cart.Clear();

                // Утвержение
                Assert.AreEqual(cart.Lines.Count(), 0);
            }

            // Добавление элемента в корзину
            [TestMethod]
            public void Can_Add_To_Cart()
            {
                Mock<ITourRepository> mock = new Mock<ITourRepository>();
                mock.Setup(m => m.GetAllTours()).Returns(new List<Tour>{
                new Tour {TourId = 1, Name = "Tour1", Type = "Type1"}
            }.AsQueryable());

                Cart cart = new Cart();

                CartController controller = new CartController(mock.Object);

                controller.AddToCart(cart, 1, null);

                Assert.AreEqual(cart.Lines.Count(), 1);
                Assert.AreEqual(cart.Lines.ToList()[0].Tour.TourId, 1);
            }

            // После добавления книги в корзину - перенаправление на страницу корзины
            [TestMethod]
            public void Adding_Tour_To_Cart_Goes_To_Cart_Screen()
            {
                Mock<ITourRepository> mock = new Mock<ITourRepository>();
                mock.Setup(m => m.GetAllTours()).Returns(new List<Tour>{
                new Tour {TourId = 1, Name = "Tour1", Type = "Type1"}
            }.AsQueryable());

                Cart cart = new Cart();

                CartController controller = new CartController(mock.Object);

                RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

                Assert.AreEqual(result.RouteValues["action"], "Index");
                Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
            }


            [TestMethod]
            public void Can_View_Cart_Contents()
            {
                Cart cart = new Cart();
                CartController target = new CartController(null);

            }
        }
    }
}
