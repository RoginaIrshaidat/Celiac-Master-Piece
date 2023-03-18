using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MasterPiece.Models;

namespace MasterPiece.Controllers
{
    [Authorize]

    public class ProductsController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.Category_id = new SelectList(db.Categories, "Category_ID", "Category_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_ID,Product_Name,Product_Description,Price,InStore,Main_Image,Image1,Image2,Image3,Quantity,Category_id")] Product product , HttpPostedFileBase Main_Image, HttpPostedFileBase Image1)
        {
            if (ModelState.IsValid)
            {
                string mainImgPath = "../ProductImages/" + Main_Image.FileName;
                //product.Main_Image = Main_Image.FileName;
                //product.Image1 = Image1.FileName;
                Main_Image.SaveAs(Server.MapPath(mainImgPath));
                product.Main_Image = Main_Image.FileName;
                //Image1.SaveAs(Server.MapPath("../Image/" + Image1));
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_id = new SelectList(db.Categories, "Category_ID", "Category_Name", product.Category_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Category_id = new SelectList(db.Categories, "Category_ID", "Category_Name", product.Category_id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,[Bind(Include = "Product_ID,Product_Name,Product_Description,Price,InStore,Main_Image,Image1,Image2,Image3,Quantity,Category_id")] Product product, HttpPostedFileBase Main_Image)
        {
            if (ModelState.IsValid)
            {
                var existingModel = db.Products.AsNoTracking().FirstOrDefault(p => p.Product_ID == id);


                if (Main_Image != null)
                {

                    string mainImgPath = Path.GetFileName(Main_Image.FileName);
                    Main_Image.SaveAs(Path.Combine(Server.MapPath("~/Image/"), Main_Image.FileName));
                    product.Main_Image = mainImgPath;

                }
                else
                {
                    product.Main_Image = existingModel.Main_Image;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_id = new SelectList(db.Categories, "Category_ID", "Category_Name", product.Category_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
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
