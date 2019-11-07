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
    public class AdminTourController : Controller
    {
        private ITourRepository repository;
        public IFormatRepository formatRepository = new EFFormatRepository();

        public AdminTourController(ITourRepository repo)
        {
            repository = repo;
        }

        public ViewResult Edit(int tourId)
        {
            Tour tour = repository.GetAllTours().FirstOrDefault(b => b.TourId == tourId);

            TourEditViewModel model = new TourEditViewModel
            {
                Tour = tour,
                Formats = formatRepository.GetAllFormats(),
                CurentFormat = tour.FormatId
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Tour tour, int formatId)
        {
            if (ModelState.IsValid)
            {
                tour.FormatId = formatId;
                repository.SaveTour(tour);
                TempData["message"] = string.Format("Зміна контенту \"{0}\" збережена", tour.Name);
                return RedirectToAction("List");
            }
            else
            {
                TourEditViewModel model = new TourEditViewModel
                {
                    Tour = tour,
                    Formats = formatRepository.GetAllFormats(),
                    CurentFormat = tour.FormatId
                };
                return View(model);
            }
        }

        public ViewResult Add()
        {
            TourAddViewModel model = new TourAddViewModel
            {
                Tour = new Tour(),
                Formats = formatRepository.GetAllFormats(),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Tour tour, int formatId)
        {
            if (ModelState.IsValid)
            {
                tour.FormatId = formatId;
                repository.SaveTour(tour);
                TempData["message"] = string.Format("Формат \"{0}\" додано", tour.Name);
                return RedirectToAction("List");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Users = "Admin")]
        public ViewResult List(int format = 0, string query = null, string type = null)
        {
            TourListViewModel model = new TourListViewModel
            {
                Tours = repository.GetAllTours()
                    .Where(b => format == 0 || b.Format.FormatId == format)
                    .Where(b => type == null || b.Type == type)
                    .Where(b => query == null || b.Name == query || b.Description == query),
                Types = repository.GetAllTours()
                    .Select(tour => tour.Type)
                    .Distinct()
                    .OrderBy(x => x),
                Formats = formatRepository.GetAllFormats(),
                CurentFormat = format,
                CurentType = type
            };

            return View(model);
        }

        //[Authorize]
        public RedirectToRouteResult Remove(int tourId)
        {
            Tour tour = repository.GetAllTours().FirstOrDefault(b => b.TourId == tourId);

            repository.RemoveTour(tour);
            TempData["message"] = string.Format("Видалення контенту успішне");

            return RedirectToAction("List");
        }
    }
}