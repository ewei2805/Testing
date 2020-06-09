using Natroral.Core.Contracts;
using Natroral.Core.Models;
using Natroral.Core.ViewModels;
using Natroral.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Natroral.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<Category> categories;

        public HomeController(IRepository<Product> productContext, IRepository<Category> categoryContext)
        {
            context = productContext;
            categories = categoryContext;
        }
        public ActionResult Index()
        {          
            return View();
        }

        public ActionResult ProductListing(string Category = null)
        {
            List<Product> products;
            List<Category> prodcategories = categories.Collection().ToList();

            if (Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.Categories = prodcategories;

            return View(model);
        }
              
        public ActionResult DetailsById(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }           
        }

        public ActionResult Details(string name)
        {
            // this allows both product name and seo name

            DataContext dbContext = new DataContext();
            Product product = new Product();

            name = name.Trim();
            if (name.IndexOf(" ") > 0)
            {
                product = dbContext.Products.Where(x => x.Name == name).FirstOrDefault();
            }
            else
            {
                product = dbContext.Products.Where(x => x.SEOName == name).FirstOrDefault();
            }
           
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(FormCollection fc) // fc is used to differentiate the two
        {
            return RedirectToAction("Index");
            //return View();
        }
    }
}