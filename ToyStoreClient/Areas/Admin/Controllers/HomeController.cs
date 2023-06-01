using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToyStoreAPI.Models;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = UserRoles.Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var productNumber=Utilities.SendDataRequest<int>(ConstantValues.Product.CountProducts);
            ViewBag.ProductNumber = productNumber;

            var categoryNumber = Utilities.SendDataRequest<int>(ConstantValues.Category.CountCategories);
            ViewBag.CategoryNumber = categoryNumber;

            var userNumber = Utilities.SendDataRequest<int>(ConstantValues.User.CountUsers);
            ViewBag.UserNumber = userNumber;

            var orderNumber = Utilities.SendDataRequest<int>(ConstantValues.Order.CountOrders);
            ViewBag.OrderNumber = orderNumber;

            return View();
        }
    }
}
