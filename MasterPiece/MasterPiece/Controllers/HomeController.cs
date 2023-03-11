using MasterPiece.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterPiece.Controllers
{
    public class HomeController : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        public ActionResult Index()
        {
            return View(db.Products.ToList());
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
            return View(db.Products.ToList());
        }
        public ActionResult Product_Details()
        {
            return View(db.Products.ToList());
        }
    }
}