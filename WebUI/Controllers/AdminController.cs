using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Users = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}