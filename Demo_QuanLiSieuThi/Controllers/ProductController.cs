using Demo_QuanLiSieuThi.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_QuanLiSieuThi.Controllers
{
    public class ProductController : Controller
    {
        private Db_QLSTEntities db = new Db_QLSTEntities();
        // GET: Product

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            var checkCategoryID = db.Categories.Find(product.CategoryID);
            var checkProducerID = db.Producers.Find(product.ProducerID);
            //Tìm kiếm theo tên
            var checkProductName = db.Products.FirstOrDefault(t => t.Name == product.Name);
            if (checkCategoryID != null && checkProducerID != null && checkProductName == null)
            {
                product.DateProduce = DateTime.Now;
                product.IsDelete = false;
                db.Products.Add(product);
                db.SaveChanges();
                return Json(true);
            }
            else if (checkProductName != null && checkProductName.IsDelete == true)
            {
                product.DateProduce = DateTime.Now;
                product.IsDelete = false;
                db.Products.Add(product);
                db.SaveChanges();
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            var editProduct = db.Products.FirstOrDefault(t => t.ProductID == product.ProductID);
            var checkCategoryID = db.Categories.Find(product.CategoryID);
            var checkProducerID = db.Producers.Find(product.ProducerID);          
            if (ModelState.IsValid)
            {
                if (checkCategoryID != null && checkProducerID != null)
                {
                    editProduct.Name = product.Name;
                    editProduct.Price = product.Price;
                    editProduct.Amount = product.Amount;
                    editProduct.Description = product.Description;
                    editProduct.CategoryID = product.CategoryID;
                    editProduct.ProducerID = product.ProducerID;
                    db.SaveChanges();
                    return Json(true);
                }
            } 
            return Json(false);
        }

        [HttpPost]
        public ActionResult DeleteProduct(Product pro)
        {
            var product = db.Products.Find(pro.ProductID);
            if (product != null)
            {
                product.IsDelete = true;
                db.SaveChanges();
                return Json(true);
            }
            return new EmptyResult();
        }

        public JsonResult SearchProductByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var products = db.Products.Where(t=>t.Name.Contains(name)).ToList();
                return Json(products);
            }
            return Json(false);
        }

    }
}