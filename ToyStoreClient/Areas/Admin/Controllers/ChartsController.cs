using Microsoft.AspNetCore.Mvc;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public List<StatisticModel> Statistic()
        {
            var productStatistics = Utilities.SendDataRequest<List<StatisticModel>>(ConstantValues.Product.ProductStatistics);
            return productStatistics;
        }
    }
}
