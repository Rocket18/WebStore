using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models.Entities;
using WebStore.Models.Abstract;

namespace WebStore.UI.Areas.Adminka.Controllers
{
    public class CategoryController : AdminController
    {
        public CategoryController(IUnitOfWork repository) : base(repository) { }
          
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cats()
        {
            var categories = repository.Categories.GetAll().ToList();
            return PartialView(categories);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "CategoryID")] Categories obj)
        {
            if (ModelState.IsValid)
            {
                repository.Categories.Insert(obj);
                repository.Save();  
                return RedirectToAction("Index");
            }
           

                return RedirectToAction("Index");
           
        }
        [HttpPost]
        public ActionResult Update(Categories obj)
        {
            if (ModelState.IsValid)
            {
                repository.Categories.Update(obj);
                repository.Save();              
                return RedirectToAction("Index");
            }
          
                return RedirectToAction("Index");
         
        }
        [HttpPost]
        public ActionResult Delete(int CategoryID)
        {

            repository.Categories.Delete(CategoryID);
            repository.Save(); 
            return RedirectToAction("Index");
        }
    }
}
