using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlexVanWolferen.MultiAI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Logger()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Log(int index)
        {
            HttpContext.Items["ai_preferred"] = index;


            System.Diagnostics.Trace.TraceInformation("Information: This is a message from the application");
            System.Diagnostics.Trace.TraceWarning("Warning: This is a message from the application");
            System.Diagnostics.Trace.TraceError("Error: This is a message from the application");

            return RedirectToAction("Logger", new { @ai = index });
        }
    }
}