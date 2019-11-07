using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private ITourRepository repository;
        public CartController(ITourRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public ViewResult Order(Cart cart, decimal sum)
        {
            IOrderRepository orderRepository = new EFOrderRepository();

            Order order = new Order
            {
                Sum = sum,
                UserId = Convert.ToInt32(User.Identity.Name)
            };
            orderRepository.SaveOrder(order);

            cart.Clear();

            return View(order.OrderId);
        }

        [Authorize]
        public RedirectToRouteResult AddToCart(Cart cart, int tourId, string returnUrl)
        {
            Tour tour = repository.GetAllTours()
                .FirstOrDefault(b => b.TourId == tourId);

            if (tour != null)
            {
                cart.AddItem(tour, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int tourId, string returnUrl)
        {
            Tour tour = repository.GetAllTours()
                .FirstOrDefault(b => b.TourId == tourId);

            if (tour != null)
            {
                cart.RemoveLine(tour);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

    }
}