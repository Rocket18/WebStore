using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebStore.Models.Entities;

using WebStore.UI.Infrastructure.Concrete;

namespace WebStore.UI.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login(string returnUrl)
        {
        
            if (Request.IsAjaxRequest())
            {
                return PartialView("_LoginModal");
            }

            ViewBag.ReturnUrl = returnUrl;
         
            return View();
        }


        [HttpPost]
        public ActionResult LoginAjax(Users users,string returnUrl="")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(users.Username, users.Password))
                {
                    bool remember = Request["remember-me"] == "1" ? true : false;
                    FormsAuthentication.SetAuthCookie(users.Username, remember);
                    return Json(new { error = false, name = returnUrl });
                }
                else
                {
                    return Json(new{error= true, name= "Невірно введене ім\'я користувача чи пароль"});
                }
            }
            else
            {
                return Json(new{error=true,name= "Невірні дані"});    
            }        
        }

        [HttpPost]
        public ActionResult Login(Users users, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(users.Username, users.Password))
                {
                    bool remember = Request["remember-me"]=="1"?true:false;
                    FormsAuthentication.SetAuthCookie(users.Username, remember);              
                    return Redirect(returnUrl ?? Url.Action("List","Product"));
                }
                else
                {
                    ViewBag.ReturnUrl = returnUrl;
                    ModelState.AddModelError("", "Невірно введене ім\'я користувача чи пароль");
                    return View(users);
                }
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;

                return View(users);
            }
        }

        [HttpPost]
        public ActionResult LogOut(string returnUrl="")
        {
            FormsAuthentication.SignOut();

            if (string.IsNullOrEmpty(returnUrl))
             return Redirect(Url.Action("List", "Product"));
            else
                return Redirect(returnUrl);
        }

    }
}
