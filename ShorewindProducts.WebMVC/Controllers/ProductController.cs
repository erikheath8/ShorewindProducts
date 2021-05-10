using Microsoft.AspNet.Identity;
using Shorewind.Data;
using Shorewind.Models.ProductModels;
using Shorewind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ShorewindProducts.WebMVC.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        private ProductService CreateProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var productService = new ProductService(userId);
            return productService;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}