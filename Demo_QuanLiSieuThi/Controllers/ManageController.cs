using Demo_QuanLiSieuThi.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_QuanLiSieuThi.Controllers
{
    public class ManageController : Controller
    {
        private Db_QLSTEntities db = new Db_QLSTEntities();
        // GET: Manage
        //Manage Page
        public ActionResult Index()
        {
            ViewBag.employees = db.Employees.ToList();
            ViewBag.products = db.Products.Where(t => t.IsDelete.Equals(false)).ToList();
            if (Session["EmployeeID"] != null)
            {
                return View();
            }
            else return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult AddEmp(Employee emp)
        {
            if (ModelState.IsValid)
            {
                var check = db.Employees.FirstOrDefault(t => t.UserName == emp.UserName);
                if (check == null)
                {
                    emp.Position = "2";
                    emp.WorkHours = 0;
                    emp.NameMarket = "Market1";
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return Json(true);
                }
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult EditEmp(Employee emp)
        {
            var editEmp = db.Employees.FirstOrDefault(t => t.EmployeeID == emp.EmployeeID);
            if (ModelState.IsValid)
            {
                if(editEmp != null)
                {           
                    editEmp.Name = emp.Name;
                    editEmp.BirthOfDay = emp.BirthOfDay;
                    editEmp.Address = emp.Address;
                    editEmp.Phone = emp.Phone;
                    editEmp.Position = emp.Position;
                    editEmp.WorkHours = emp.WorkHours;
                    db.SaveChanges();
                    return Json(true);
                }             
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult DeleteEmp(Employee emp)
        {
            var employee = db.Employees.Where(t => t.EmployeeID == emp.EmployeeID).FirstOrDefault();
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                return Json(true);
            }
            return new EmptyResult();
        }


    }
}