using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using isitsupported.xyz.Models;

namespace isitsupported.xyz.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient ApiRequester { get; set; }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            ApiRequester = new HttpClient();
            ApiRequester.BaseAddress = new Uri("http://localhost/api");
        }

        public IActionResult Index()
        {
            var EnumerateResults = ApiRequester.GetStringAsync("/Enumerate");
            return View();
        }
        public IActionResult About()
        {
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
