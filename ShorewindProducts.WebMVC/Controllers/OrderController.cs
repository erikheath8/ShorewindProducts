using Microsoft.AspNet.Identity;
using Shorewind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShorewindProducts.WebMVC.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public OrderService CreateService()
        {
            var _userId = Guid.Parse(User.Identity.GetUserId());
            var orderService = new OrderService(_userId);
            return orderService;
        }

        // GET: OrderList
        public ActionResult Index()
        {
            var orders = CreateService().GetOrders();
            return View(orders);
        }

        // GET: Orders
        public ActionResult Create()
        {
            return View();
        }

    }
}