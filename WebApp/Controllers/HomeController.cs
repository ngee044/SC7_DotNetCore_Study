using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private Northwind db;

        public HomeController(Northwind injectedContext)
        {
            db = injectedContext;
        }

        public IActionResult Index()
        {
#if false
            //같은 코드
            var model = new HomeIndexViewModel();
            model.VisitorCount = (new Random()).Next(1, 1001);
            model.Products = db.Products.ToArray();
#else
            //modern style
            var model = new HomeIndexViewModel
            { VisitorCount = (new Random()).Next(1, 1001), Products = db.Products.ToArray() };
#endif
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
