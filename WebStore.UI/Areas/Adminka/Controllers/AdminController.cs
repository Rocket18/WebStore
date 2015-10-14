using System;
using System.Web;
using System.Web.Mvc;
using WebStore.Models.Abstract;

namespace WebStore.UI.Areas.Adminka.Controllers
{
   [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        protected IUnitOfWork repository;

        public AdminController(IUnitOfWork repo)
        {
            repository = repo;
        }

    }
}
