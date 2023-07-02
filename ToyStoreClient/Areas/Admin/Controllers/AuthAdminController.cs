using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToyStoreAPI.Models;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = UserRoles.Admin)]
    public class AuthAdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var token = Utilities.SendDataRequest<TokenModel>(ConstantValues.Authenticate.Login, model);

            if (string.IsNullOrEmpty(token?.Token))
            {
                ModelState.AddModelError("", "Đăng nhập không hợp lệ. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.Set("Token", token.Token);

            return RedirectToAction("Index", "Home", new { area = "Admin" });

        }
    }
}
