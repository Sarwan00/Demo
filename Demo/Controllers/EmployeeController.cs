using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        [HttpGet]
        public ActionResult Index()
        {
            DataAccessLayer Data = new DataAccessLayer();
            List<Employee> Employees = Data.GetEmployeeDetails().ToList();
            return View(Employees);
        }
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Index_Get()
        {
            return View("Index_Get");
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Index_Post(FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer();
                Employee employee = new Employee();
                employee.Name = formCollection["name"];
                employee.Gender = formCollection["Gender"];
                employee.City = formCollection["City"];
                employee.DateOfBirth = DateTime.Parse(formCollection["DateOfBirth"]);
                dataAccessLayer.InsertEmployee(employee);
                return RedirectToAction("Index");

            }
            else
            {
                return View("Index_Get");
            }
        }
    }
}