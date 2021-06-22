using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JooleApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Compare(List<int> productIDs)
        {
            Service service = new Service();
            ViewBag.productDetails = new List<Dictionary<string, Dictionary<string, string>>>();
            foreach (int productID in productIDs)
            {
                Dictionary<string, Dictionary<string, string>> productDetails = service.getProductDetailsByID(productID);
                ViewBag.productDetails.Add(productDetails);
            }
            return View();
        }
    }
}