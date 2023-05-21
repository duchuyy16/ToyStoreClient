using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using ToyStoreClient.ApiServices;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //private readonly Client _client;

        //public HomeController(ILogger<HomeController> logger, Client client)
        //{
        //    _logger = logger;
        //    _client = client;
        //}

        public IActionResult Index()
        {
            try
            {
                //var a = _client.GetAllProductsBestSellers();
                //return View(a);
                var products = Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProductsBestSellers);
                return View(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
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