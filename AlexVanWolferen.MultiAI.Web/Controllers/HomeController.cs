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

        public ActionResult Reggol()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Log(int index, string message)
        {
            HttpContext.Items["ai_preferred"] = index;


            System.Diagnostics.Trace.TraceInformation($"Information {index}: {(string.IsNullOrWhiteSpace(message) ? "This is a message from the application" : message)}");
            System.Diagnostics.Trace.TraceWarning($"Warning {index}: {(string.IsNullOrWhiteSpace(message) ? "This is a message from the application" : message)}");
            System.Diagnostics.Trace.TraceError($"Error {index}: {(string.IsNullOrWhiteSpace(message) ? "This is a message from the application" : message)}");

            return RedirectToAction("Logger", new { @ai = index });
        }

        [HttpPost]
        public ActionResult Reg()
        {
            string response = string.Empty;

            using (var client = new System.Net.WebClient())
            {
                client.Headers.Add("ai-callback", System.Configuration.ConfigurationManager.AppSettings["ai_primary"]);
                response = client.DownloadString("http://localhost:55809/WeatherForecast");
            }

            return View("Reggol", new ViewModels.Rellog { Response = response });
        }

    }
}