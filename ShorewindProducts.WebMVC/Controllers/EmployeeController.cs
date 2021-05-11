using Microsoft.AspNet.Identity;
using Shorewind.Models.EmployeeModels;
using Shorewind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShorewindProducts.WebMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeService CreateService()
        {
            var _userId = Guid.Parse(User.Identity.GetUserId());
            var employeeService = new EmployeeService(_userId);
            return employeeService;
        }

        // GET: Employee
        public ActionResult Index()
        {
            var employees = CreateService().GetEmployees();

            return View(employees);
        }

        // GET: Employee
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ActionName("Create")]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeCreate customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var service = CreateService();

            if (service.CreateEmployee(customer))
            {
                ViewBag.SaveResult = "Your Employee was created.";
            };

            ModelState.AddModelError("", "Employee could not be created.");

            return RedirectToAction("EmployeeList");
        }

        // Get: Customer/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var service = CreateService();
            service.DeleteEmployee(id);

            TempData["SaveResult"] = "Your note was deleted.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var service = CreateService();
            var employee = service.GetEmployeeById(id);
            var model = new EmployeeEdit
            {
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                ModifiedUtc = employee.ModifiedUtc,
            };
            return View(model);
        }

        // POST: Restaurant/Edit/{id}
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeEdit employee)
        {
            if (ModelState.IsValid)
            {
                var service = CreateService();
                service.UpdateEmployee(employee);
            }
            return RedirectToAction("Index");
        }

        // GET: Restaurant/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateService();
            var employee = service.GetEmployeeById(id);

            return View(employee);
        }

    }
}