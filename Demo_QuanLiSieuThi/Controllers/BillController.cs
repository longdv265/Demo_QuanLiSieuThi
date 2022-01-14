using Demo_QuanLiSieuThi.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_QuanLiSieuThi.Controllers
{
    public class BillController : Controller
    {
        private Db_QLSTEntities db = new Db_QLSTEntities();
        // GET: Bill
        public ActionResult Index()
        {
            if (Session["EmployeeID"] != null || Session["BillID"] != null)
            {
                var billID = (int)Session["BillID"];
                ViewBag.products = db.BillDetails.Where(t => t.BillID == billID).ToList();
                ViewBag.productDetails = db.Products.ToList();
                return View();
            }
            else return RedirectToAction("Login", "Home");
        }

        public ActionResult CreateBill(Bill bill)
        {
            bill.EmployeeID = (int)Session["EmployeeID"];
            db.Bills.Add(bill);
            db.SaveChanges();
            //Get Session by Bill ID
            var billId = db.Bills.Select(t => t.BillID).Max();
            Session["BillID"] = billId;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult AddProductToBill(Product pro)
        {
            var product = db.Products.Where(t => t.ProductID == pro.ProductID).FirstOrDefault();
            //Tìm kiếm trong BillDetail sản phẩm có cùng ProductID và BillID
            var billID = (int)Session["BillID"];
            var checkBillDetail = db.BillDetails.Where(t => t.BillID == billID && t.ProductID == pro.ProductID).FirstOrDefault();

            if (product != null && pro.Amount <= product.Amount)
            {
                //Kiểm tra xem mặt hàng đã có trong hóa đơn chưa
                if (checkBillDetail != null)
                {

                    checkBillDetail.Amount += pro.Amount;
                    checkBillDetail.UnitPrice += product.Price * pro.Amount;
                }
                else
                {
                    var billDetail = new BillDetail
                    {
                        BillID = billID,
                        ProductID = pro.ProductID,
                        Type = "",
                        Amount = pro.Amount,
                        UnitPrice = product.Price * pro.Amount,
                        Date = DateTime.Now,
                    };
                    db.BillDetails.Add(billDetail);
                }
                product.Amount -= pro.Amount;
                if (product.Amount == 0) product.Description = "Sold out";
                db.SaveChanges();
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult EditProductInBill(Product pro)
        {
            var billID = (int)Session["BillID"];
            var product = db.Products.FirstOrDefault(t => t.ProductID == pro.ProductID);
            var productInBill = db.BillDetails.Where(t => t.ProductID == pro.ProductID && t.BillID == billID).FirstOrDefault();
            if (productInBill != null && pro.Amount <= (product.Amount + productInBill.Amount))
            {
                product.Amount -= (pro.Amount - productInBill.Amount);

                if (product.Amount == 0) product.Description = "Sold out";
                else product.Description = "";

                productInBill.Amount = pro.Amount;
                productInBill.UnitPrice = pro.Amount * product.Price;

                if (productInBill.Amount == 0)
                {
                    db.BillDetails.Remove(productInBill);
                }
                db.SaveChanges();
                return Json(true);
            }
            return Json(false);
        }
        public ActionResult Pay()
        {
            Session["BillID"] = null;
            return RedirectToAction("Index", "Manage");
        }
    }
}