using Demo_QuanLiSieuThi.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_QuanLiSieuThi.Controllers
{
    public class HomeController : Controller
    {
        private Db_QLSTEntities db = new Db_QLSTEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                var data = db.Employees.Where(t => t.UserName.Equals(userName) && t.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //Add session
                    Session["EmployeeID"] = data.FirstOrDefault().EmployeeID;
                    Session["Address"] = data.FirstOrDefault().Address;
                    Session["Name"] = data.FirstOrDefault().Name;
                    Session["BirthOfDay"] = data.FirstOrDefault().BirthOfDay.ToString("dd-MM-yyyy");
                    Session["Phone"] = data.FirstOrDefault().Phone;
                    Session["Position"] = data.FirstOrDefault().Position;
                    Session["WorkHours"] = data.FirstOrDefault().WorkHours;
                    Session["NameMarket"] = data.FirstOrDefault().NameMarket;
                    return RedirectToAction("Index","Manage");
                }
                else
                {
                    ViewBag.error = "Login Fail";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
       
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}