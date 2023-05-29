using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var response = Utilities.SendDataRequest<Response>(ConstantValues.Authenticate.Register, model);

                if (response.Status == "Success")
                {
                    TempData["StateRegister"] = "Success";
                    TempData["MessageRegister"] = "User created successfully!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.Message!);
                }                    
            }
            return View(model);
        }

        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterAdmin(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var response = Utilities.SendDataRequest<Response>(ConstantValues.Authenticate.Register, model);

                if (response.Status == "Success")
                {
                    TempData["StateRegister"] = "Success";
                    TempData["MessageRegister"] = "User created successfully!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.Message!);
                }
            }
            return View(model);
        }
    }
}
