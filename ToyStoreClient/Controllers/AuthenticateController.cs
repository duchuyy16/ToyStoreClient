using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ToyStoreAPI.Models;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Controllers
{
    public class AuthenticateController : Controller
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
            HttpContext.Session.Set("UserInfo", token.Token.ConvertJwtToJsonObject());

            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            return View(model);
        }

        //[HttpPost]
        //[Route("register-admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        //{
        //    return View(model);
        //}
    }
}
