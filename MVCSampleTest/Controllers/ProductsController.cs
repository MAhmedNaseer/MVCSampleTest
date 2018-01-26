using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCSampleTest.Models;

namespace MVCSampleTest.Controllers
{
    public class ProductsController : Controller
    {
        private MVCSampleTestEntities db = new MVCSampleTestEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Picture);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.ProductPictureId = new SelectList(db.Pictures, "PictureID", "PicturePath");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,ProductDescription,ProductPrice,ProductCreatedDate,ProductModifiedDate,ProductCategoryId,ProductQuantity,ProductPictureId")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (!product.ProductQuantity.HasValue)
                    product.ProductQuantity = 0;
                product.ProductCreatedDate = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryId = new SelectList(db.Categories, "CategoryID", "CategoryName", product.ProductCategoryId);
            ViewBag.ProductPictureId = new SelectList(db.Pictures, "PictureID", "PicturePath", product.ProductPictureId);
 //           ViewBag.Pro
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(db.Categories, "CategoryID", "CategoryName", product.ProductCategoryId);
            ViewBag.ProductPictureId = new SelectList(db.Pictures, "PictureID", "PicturePath", product.ProductPictureId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,ProductDescription,ProductPrice,ProductCreatedDate,ProductModifiedDate,ProductCategoryId,ProductQuantity,ProductPictureId")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductCreatedDate = new MVCSampleTestEntities().Products.FirstOrDefault(x => x.ProductID == product.ProductID).ProductCreatedDate;
                product.ProductModifiedDate = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryId = new SelectList(db.Categories, "CategoryID", "CategoryName", product.ProductCategoryId);
            ViewBag.ProductPictureId = new SelectList(db.Pictures, "PictureID", "PicturePath", product.ProductPictureId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
