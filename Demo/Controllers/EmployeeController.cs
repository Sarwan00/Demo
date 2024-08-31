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
        public ActionResult Index()
        {
            DataAccessLayer Data = new DataAccessLayer();
            List<Employee> Employees = Data.GetEmployeeDetails().ToList();
            return View(Employees);
        }
    }
}