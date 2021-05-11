using Microsoft.AspNet.Identity;
using Shorewind.Data;
using Shorewind.Models.ProductModels;
using Shorewind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShorewindProducts.WebMVC.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        private ProductService CreateService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var productService = new ProductService(userId);
            return productService;
        }

        // GET: Product
        public ActionResult Index()
        {
            var products = CreateService().GetProducts();
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ActionName("Create")]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreate product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var service = CreateService();

            if (service.CreateProduct(product))
            {
                ViewBag.SaveResult = "Your product was created.";
            };

            ModelState.AddModelError("", "Product could not be created.");

            return RedirectToAction("ProductList");
        }

        // Get: Customer/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var service = CreateService();
            service.DeleteProduct(id);

            TempData["SaveResult"] = "Your Product was deleted.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var service = CreateService();
            var product = service.GetProductById(id);
            var model = new ProductEdit
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                CategoryId = product.CategoryId,
                StockQuantity = product.StockQuantity,
                UnitPrice = product.UnitPrice
            };
            return View(model);
        }

        // POST: Restaurant/Edit/{id}
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEdit product)
        {
            if (ModelState.IsValid)
            {
                var service = CreateService();
                service.UpdateProduct(product);
            }
            return RedirectToAction("Index");
        }

        // GET: Restaurant/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateService();
            var product = service.GetProductById(id);

            return View(product);
        }

    }
}