using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToyStoreAPI.Models;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var productNumber=Utilities.SendDataRequest<Response>(ConstantValues.Product.CountProducts);
            //ViewBag.ProductNumber = productNumber;

            //var categoryNumber = Utilities.SendDataRequest<int>(ConstantValues.Category.CountCategories);
            //ViewBag.CategoryNumber = categoryNumber;

            //var userNumber = Utilities.SendDataRequest<int>(ConstantValues.User.CountUsers);
            //ViewBag.UserNumber = userNumber;

            //var orderNumber = Utilities.SendDataRequest<int>(ConstantValues.Order.CountOrders);
            //ViewBag.OrderNumber = orderNumber;

            //var productStatistics = Utilities.SendDataRequest<List<StatisticModel>>(ConstantValues.Product.ProductStatistics);
            //ViewBag.ProductStatistics = productStatistics;

            var count = Utilities.SendDataRequest<Count>(ConstantValues.Home.Count);
            ViewBag.ProductNumber = count.ProductCount;
            ViewBag.CategoryNumber = count.CategoryCount;
            ViewBag.UserNumber = count.UserCount;
            ViewBag.OrderNumber = count.OrderCount;
            return View();
        }
    }
}
