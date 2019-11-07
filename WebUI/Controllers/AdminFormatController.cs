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
    public class AdminFormatController : Controller
    {
        //private EFFormatRepository repository = new EFFormatRepository();

        private IFormatRepository repository;

        public AdminFormatController(IFormatRepository repo)
        {
            repository = repo;
        }

        [Authorize(Users = "Admin")]
        public ViewResult List(string query = null)
        {
            FormatListViewModel model = new FormatListViewModel
            {
                Formats = repository.GetAllFormats().Where(b => query == null || b.FormatName == query)
            };

            return View(model);
        }

        public ViewResult Edit(int formatId)
        {
            Format format = repository.GetAllFormats().FirstOrDefault(b => b.FormatId == formatId);

            return View(format);
        }

        [HttpPost]
        public ActionResult Edit(Format format)
        {
            if (ModelState.IsValid)
            {
                repository.SaveFormat(format);
                TempData["message"] = string.Format("Зміна формату \"{0}\" збережена", format.FormatName);
                return RedirectToAction("List");
            }
            else
            {
                return View(format);
            }
        }

        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Format format)
        {
            TempData["message"] = string.Format(format.FormatId.ToString());

            if (ModelState.IsValid)
            {
                repository.SaveFormat(format);
                TempData["message"] = string.Format("Формат \"{0}\" додано", format.FormatName);
                return RedirectToAction("List");
            }
            else
            {
                return View();
            }
        }

        //[Authorize]
        public RedirectToRouteResult Remove(int formatId)
        {
            Format format = repository.GetAllFormats().FirstOrDefault(b => b.FormatId == formatId);
            if (format.Tours.Count() != 0)
            {
                TempData["err-message"] = string.Format("Видалення формату неможливе: існує контент даного формату");
            }
            else
            {
                repository.RemoveFormat(format);
                TempData["message"] = string.Format("Видалення формату успішне");
            }



            return RedirectToAction("List");
        }
    }
}