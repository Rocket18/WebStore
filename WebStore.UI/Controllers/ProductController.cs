using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models.Abstract;
using WebStore.Models.Entities;
using WebStore.Models.DAL;
using System.Data.Entity;
using WebStore.UI.Models;



namespace WebStore.UI.Controllers
{
    public class ProductController : Controller
    {

        public int PageSize = 5;
        private int OtherProduct = 10;
        private IUnitOfWork repository;

        public ProductController(IUnitOfWork repositories)
        {
            repository = repositories;
        }

        public ActionResult List(int? category, int page = 1)
        {

            var products = repository.Products.GetAll();


            if (category != null)
            {
                products = products.Where(a => a.Categories
                                     .Any(c => c.CategoryID == category))
                                     .OrderByDescending(a => a.ProductID);
            }

            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Products = products.OrderByDescending(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PaginInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.GetAll().Count() : products.Count()
                },
                CurrentCategory = category

            };

            return View(viewModel);
        }

        public ViewResult GetOtherProduct(int productID)
        {
            var categories = repository.Products.GetAll(includeProperties: "Categories", filter: p => p.ProductID == productID).SingleOrDefault();

            var Cat = new List<Int32>();//Список ID до яких належить продукт
            foreach (var c in categories.Categories)
            {
                Cat.Add(c.CategoryID);
            }
            //Інші продукти
            var product = repository.Products.GetAll(includeProperties: "Categories", filter: p => (p.ProductID != productID && p.Categories.Any(c => Cat.Contains(c.CategoryID)))).Take(OtherProduct);

            return View(product);
        }

        public ActionResult View(int id)
        {
          
            var data = repository.Products.FindById(id);
            if (data == null)
                throw new HttpException(404, "Not found");
            return View(data);
        }

    }
}
