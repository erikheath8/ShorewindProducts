using Shorewind.Data;
using Shorewind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Shorewind.Models.CustomerModels;

namespace ShorewindProducts.WebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        private CustomerService CreateService()
        {
            var _userId = Guid.Parse(User.Identity.GetUserId());
            var customerService = new CustomerService(_userId);
            return customerService;
        }

        // GET: CustomerList
        public ActionResult Index()
        {
            var customers = CreateService().GetCustomers();

            return View(customers);
        }

        // GET: Customer
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ActionName("Create")]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCreate customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer); 
            }

            var service = CreateService();

            if (service.CreateCustomer(customer))
            {
                ViewBag.SaveResult = "Your note was created.";
            };

            ModelState.AddModelError("", "Note could not be created.");

            return RedirectToAction("CustomerList");
        }

        // Get: Customer/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var service = CreateService();
            service.DeleteCustomer(id);

            TempData["SaveResult"] = "Your note was deleted.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var service = CreateService();
            var customer = service.GetCustomerById(id);
            var model = new CustomerEdit
            {

            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = customer.Address,
            City = customer.City,
            State = customer.State,
            PostalCode = customer.PostalCode,
            PhoneNumber = customer.PhoneNumber,
            ModifiedUtc = customer.ModifiedUtc,
        };
            return View(model);
        }

        // POST: Restaurant/Edit/{id}
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerEdit customer)
        {
            if (ModelState.IsValid)
            {
                var service = CreateService();
                service.UpdateCustomer(customer);
            }
            return RedirectToAction("Index");
        }

        // GET: Restaurant/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateService();
            var customer = service.GetCustomerById(id);
           
            return View(customer);
        }



    }
}