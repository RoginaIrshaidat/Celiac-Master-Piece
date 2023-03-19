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
            var category = db.Categories.ToList();
            var product = db.Products.ToList();
            var data = Tuple.Create(product, category);

            return View(data);
        }
        public ActionResult singleCategory(int? id)
        {
            var category = db.Categories.ToList();
            var product = db.Products.Where(c => c.Category.Category_ID == id).ToList();
            var data = Tuple.Create(product, category);
           
            return View(data
                );
        }
        public ActionResult Product_Details(int? id)
        {
            var singleProduct=db.Products.Where(p=>p.Product_ID==id);
            return View(singleProduct);
        }
        [Authorize]
        public ActionResult AddtoCart(int? id)
        {
            HttpCookie addToCart = Request.Cookies["addToCart"];
            List<Product> allProducts = new List<Product>();
            if (addToCart != null)
            {
                List<string> items = new List<string>(addToCart.Value.Split('|'));
                dynamic ClientProducts = new Product();

                foreach (var product in items)
                {
                    int productID = Convert.ToInt32(product);
                    List<Product> itemInCart = db.Products.Where(p => p.Product_ID == productID).ToList();
                    allProducts.AddRange(itemInCart);
                }
                ClientProducts = allProducts;
                return View(ClientProducts);
            }
            else
            {
                IEnumerable<Product> model = new List<Product>();
                return View(model);
            }
        }
        public ActionResult AddProduct(int? id, string returnUrl)
        {
            HttpCookie cart = new HttpCookie("cart");

            List<string> items = new List<string>();
            if (Request.Cookies["cart"] != null)
            {
                items = new List<string>(Request.Cookies["cart"].Value.Split('|'));
            }

            items.Add(id.ToString());

            cart.Value = string.Join("|", items);
            cart.Expires = DateTime.Now.AddDays(5);
            Response.Cookies.Add(cart);

            return Redirect(returnUrl);
        }


        public ActionResult CheckOut(int? id)
        {
            
            return View();
        }
    }
}