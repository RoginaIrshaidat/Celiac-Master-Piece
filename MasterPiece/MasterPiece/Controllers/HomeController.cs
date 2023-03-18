using MasterPiece.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MasterPiece.Controllers
{
    public class HomeController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        public ActionResult userProfile()
        {
            var userID = User.Identity.GetUserId();
            var user = db.AspNetUsers.FirstOrDefault(a => a.Id == userID);

            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Shop()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult singleCategory(int? id)
        {
            var category =db.Products.Where(c=>c.Category.Category_ID==id);
           
            return View(category);
        }
        public ActionResult Product_Details(int? id)
        {
            var singleProduct=db.Products.Where(p=>p.Product_ID==id);
            return View(singleProduct);
        }
        [Authorize]
        public ActionResult AddtoCart(int? id)
        {
            var cartItem = db.Carts.SingleOrDefault(p => p.Product.Product_ID == id);
            if (cartItem==null)
            {

            }               
            return View();
        }
        public ActionResult CheckOut(int? id)
        {
            
            return View();
        }
    }
}