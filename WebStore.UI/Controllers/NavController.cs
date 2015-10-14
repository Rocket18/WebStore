using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models.Abstract;

namespace WebStore.UI.Controllers
{
    public class NavController : Controller
    {
        private IUnitOfWork repository;

        public NavController(IUnitOfWork rep)
        {
            repository = rep;
        }
        public ActionResult Menu(int? category)
        {
            ViewBag.SelectedCategory = category;
            var categories = repository.Categories.GetAll();


            return View(categories);
        }
        public ActionResult AdminMenu(int? category)
        {
            ViewBag.SelectedCategory = category;
            var categories = repository.Categories.GetAll();
            return View(categories);
        }
    }
}
