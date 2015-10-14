using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models.Entities;
using WebStore.Models.Abstract;
using Newtonsoft.Json;


namespace WebStore.UI.Areas.Adminka.Controllers
{
    public class UsersController : AdminController
    {
        public UsersController(IUnitOfWork repository) : base(repository) { }

        public ActionResult Index()
        {
            var users = repository.Users.GetAll(includeProperties: "UsersRole");
            return View(users);
        }


        [HttpGet]
        public ActionResult UpdateForm()
        {
            RolesDropDownList();
            return PartialView();
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var user = repository.Users.FindById(id);
            if(user==null)
                throw new HttpException(404, "Not found");
            JsonSerializerSettings config = new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore };
            JsonConvert.SerializeObject(user, Formatting.None, config);
            user.UsersRole = null;
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(Users obj)
        {

            if (ModelState.IsValid)
            {
                repository.Users.Update(obj);
                repository.Save();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
            
        }
        [HttpPost]
        public ActionResult Delete(int UserID)
        {
            repository.Users.Delete(UserID);
            repository.Save();
            return RedirectToAction("Index");
        }
        private void RolesDropDownList()
        {
            var users = repository.UsersRole.GetAll();

            ViewBag.Roles = new SelectList(users, "RoleID", "Name");
        }
    }
}
