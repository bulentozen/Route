using Route.Models.Data;
using Route.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Route.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        NORTHWNDEntities db = new NORTHWNDEntities();
        [HttpGet]
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            ProductsModel model = new ProductsModel();
            model.plist = db.Products.ToList();
            model.clist = db.Categories.Select(x => new CategoriesSelect { CategoryID = x.CategoryID, CategoryName = x.CategoryName }).ToList();
            model.slist = db.Suppliers.Select(x => new SuppliersSelect { SupplierID = x.SupplierID, CompanyName = x.CompanyName }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Detay(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Products product = db.Products.Find(id);
            //var products = db.Products.Select(x => new
            //{
            //    x.ProductID,
            //    x.ProductName,
            //    x.QuantityPerUnit,
            //    x.UnitPrice,
            //    x.Discontinued
            //}).Where(x => x.ProductID == id).FirstOrDefault();
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Guncel(Products product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Ekle(Products product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Added;
                db.SaveChanges();
                return null;
            }
            else
            {
                return Json(ModelState.Keys.SelectMany(x => ModelState[x].Errors).Select(m => m.ErrorMessage).ToArray(), JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult Sil(int id)
        {
            Products product = db.Products.Find(id);
            db.Entry(product).State = EntityState.Deleted;
            db.SaveChanges();
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}