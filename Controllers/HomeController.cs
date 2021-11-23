using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using isitsupported.xyz.Models;

namespace isitsupported.xyz.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // FIXME(will) This is dumb and I probably shouldn't be doing this
        private ApiController Api = new ApiController(null);
        public ProductModel Products = new ProductModel();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string product)
        {
            string DisplayResults = "";
            
            if(null != product)
            {
                DisplayResults = Api.Query(product);
                var Model = JsonConvert.DeserializeObject<ProductModel>(DisplayResults);
                return View("Product", Model);
            }
            else
            {
                DisplayResults = Api.Enumerate();
                var Model = JsonConvert.DeserializeObject<List<ProductModel>>(DisplayResults);
                return View(Model);
            }
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
