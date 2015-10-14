using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models.Abstract;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using WebStore.Models.Entities;
using WebStore.UI.Models;
using WebStore.Models.DAL;
using System.Data.Entity.Validation;

namespace WebStore.UI.Areas.Adminka.Controllers
{
    public class ProductController : AdminController
    {
        public ProductController(IUnitOfWork repository) : base(repository) { }

        public int PageSize = 4;

        public ActionResult Index(int? category, int page = 1)
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
                Products = products.OrderBy(p => p.ProductID)
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

        [HttpPost]
        public ActionResult UploadFile()
        {

            HttpPostedFileBase myFile = Request.Files["Image"];
            bool isUploaded = false;
            string message = "File upload failed";

            String[] allowedExtensions = { ".png", ".jpeg", ".jpg" };
            String fileExtension = System.IO.Path.GetExtension(myFile.FileName).ToLower();

            if (myFile != null && myFile.ContentLength != 0)
            {
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        string pathForSaving = Server.MapPath("~/Content/");

                        try
                        {
                            myFile.SaveAs(Path.Combine(pathForSaving, myFile.FileName));//                          
                            string filePath = CreateThumbnail(240, 240, pathForSaving + myFile.FileName);
                            isUploaded = true;
                            message = filePath;
                            FileInfo file = new FileInfo(pathForSaving + myFile.FileName);
                            if (file.Exists)
                                file.Delete();
                        }
                        catch (Exception ex)
                        {
                            message = string.Format("File upload failed: {0}", ex.Message);
                        }
                        break;
                    }
                }

            }
            return Json(new { isUploaded = isUploaded, message = message }, "text/html");
        }

        private string CreateThumbnail(int maxWidth, int maxHeight, string path)
        {

            var image = System.Drawing.Image.FromFile(path);
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            Graphics thumbGraph = Graphics.FromImage(newImage);

            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            //thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

            thumbGraph.DrawImage(image, 0, 0, newWidth, newHeight);
            image.Dispose();
            string pathForSaving = Server.MapPath("~/Content/gallery/");

            string FileName = "Image" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString() + Path.GetExtension(path);
            string fileRelativePath = pathForSaving + FileName;
            newImage.Save(fileRelativePath, newImage.RawFormat);

            return FileName;



        }


        [HttpGet]
        public ActionResult Create()
        {
            var model = new Products { Image = "/gallery/Image_Not_Found.jpg" };
            CategoryDropDownList();
            StatusDropDownList();
            return View(model);
        }

        private void StatusDropDownList()
        {
            var Status = repository.Status.GetAll().OrderBy(c => c.Name);
            ViewBag.Status = new SelectList(Status, "StatusID", "Name");
        }
        private void CategoryDropDownList(IQueryable<int> selectedCategories = null)
        {
            var Category = repository.Categories.GetAll().OrderBy(c => c.Name);
            ViewBag.Categories = new MultiSelectList(Category, "CategoryID", "Name", selectedCategories);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "code")]  Products product, string[] selectedCategories)
        {

            if (selectedCategories != null)
            {
                product.Categories = new List<Categories>();

                foreach (var cat in selectedCategories)
                {
                    var catToAdd = repository.Categories.FindById(int.Parse(cat));
                    product.Categories.Add(catToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                product.Code = Guid.NewGuid();
                repository.Products.Insert(product);
                repository.Save();
                return RedirectToAction("Index");
            }

            CategoryDropDownList();
            StatusDropDownList();
            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var product = repository.Products.FindById(id);
            if (product == null)
                throw new HttpException(404, "Not found");
            var selectedCategory = product.Categories.Select(c => c.CategoryID).AsQueryable();
            StatusDropDownList();
            CategoryDropDownList(selectedCategory);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Products product, string[] selectedCategories)
        {
            var DBproduct = repository.Products.GetAll(includeProperties: "Categories", filter: p => p.ProductID == product.ProductID).SingleOrDefault();
            if (ModelState.IsValid)
            {
                TryUpdateModel(DBproduct, "",new string[] { "Name", "Image", "Price", "Avilability", "Company" });
                UpdateProductCategory(selectedCategories, DBproduct);
                repository.Products.Update(DBproduct);
                repository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                StatusDropDownList();
                CategoryDropDownList();
                return View(product);
            }
        }

        private void UpdateProductCategory(string[] selectedCategories, Products DBproduct)
        {
            if (selectedCategories == null)
            {
                DBproduct.Categories = new List<Categories>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var productCategory = new HashSet<int>(DBproduct.Categories.Select(c => c.CategoryID));
      
            foreach (var cat in repository.Categories.GetAll())
            {
                if (selectedCategoriesHS.Contains(cat.CategoryID.ToString()))
                {
                    if (!productCategory.Contains(cat.CategoryID))
                    {
                        DBproduct.Categories.Add(cat);
                    }
                }
                else
                {
                    if (productCategory.Contains(cat.CategoryID))
                    {
                        DBproduct.Categories.Remove(cat);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int productID)
        {
            repository.Products.Delete(productID);
            repository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var product = repository.Products.FindById(id);
            if(product==null)
                throw new HttpException(404, "Not found");
           
            return View(product);
        }

    }
}
