using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AdminOrderController : Controller
    {
        //private EFOrderRepository repository = new EFOrderRepository();

        private IOrderRepository repository;

        public AdminOrderController(IOrderRepository repo)
        {
            repository = repo;
        }

        [Authorize(Users = "Admin")]
        public ViewResult List(int query = 0)
        {
            OrderListViewModel model = new OrderListViewModel
            {
                Orders = repository.GetAllOrders().Where(b => query == 0 || b.OrderId == query)
            };

            return View(model);
        }

        //[Authorize]
        //public ViewResult Edit(int orderId)
        //{
        //    Order order = repository.GetAllOrders().FirstOrDefault(b => b.OrderId == orderId);

        //    return View(order);
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult Edit(Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repository.SaveOrder(order);
        //        TempData["message"] = string.Order("Зміна формату \"{0}\" збережена", order.Name);
        //        return RedirectToAction("List");
        //    }
        //    else
        //    {
        //        return View(order);
        //    }
        //}

        //[Authorize]
        //public ViewResult Add()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult Add(Order order)
        //{
        //    TempData["message"] = string.Order(order.OrderId.ToString());

        //    if (ModelState.IsValid)
        //    {
        //        repository.SaveOrder(order);
        //        TempData["message"] = string.Order("Формат \"{0}\" додано", order.Name);
        //        return RedirectToAction("List");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public RedirectToRouteResult Remove(int orderId)
        {
            Order order = repository.GetAllOrders().FirstOrDefault(b => b.OrderId == orderId);

            repository.RemoveOrder(order);
            TempData["message"] = "Видалення формату успішне";

            return RedirectToAction("List");
        }
    }
}