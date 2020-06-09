using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Natroral.WebUI.Controllers
{
    public class BlogController : Controller
    {      
        // GET: Blog
        public ActionResult BlogList()
        {
            return View();
        }

        public ActionResult BlogSidebarPartial()
        {
            return PartialView();
        }

        public ActionResult MintForTeeth()
        {
            return View();
        }

        public ActionResult CleanTeeth()
        {
            return View();
        }

        public ActionResult HowItWorks()
        {
            return View();
        }
    }
}