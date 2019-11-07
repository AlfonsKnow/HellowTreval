using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ToursController : Controller
    {
        private ITourRepository repository;
        public int pageSize = 4;

        public ToursController(ITourRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string type, int page = 1)
        {
            ToursListViewModel model = new ToursListViewModel
            {
                Tours = repository.GetAllTours()
                .Where(b => type == null || b.Type == type)
                 .OrderBy(tour => tour.TourId)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = type == null ?
                        repository.GetAllTours().Count() :
                        repository.GetAllTours().Where(tour => tour.Type == type).Count()
                },
                CurrentType = type
            };

            return View(model);
        }
    }
}