﻿using Microsoft.AspNetCore.Authentication;
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

            if (token == null || string.IsNullOrEmpty(token.Token))
            {
                TempData["Error"] = $"<h5>Invalid login</h5>" +
                            "Please double check your username and password.";

                return View(model);
            }

            HttpContext.Session.Set("Token", token.Token);
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Logout()
        {
            Utilities.SendDataRequest<Response>(ConstantValues.Authenticate.Logout);
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

        public IActionResult ResetPasswordToken()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPasswordToken(string email)
        {
            var url = string.Format(ConstantValues.Authenticate.ResetPasswordToken, email);
            var response = Utilities.SendDataRequest<Response>(url);

            return RedirectToAction("Login", "Authenticate");
        }

        [Route("reset-password")]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            var response = Utilities.SendDataRequest<Response>(ConstantValues.Authenticate.ResetPassword, model);
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public IActionResult ResetPassword(ResetPasswordModel model)
        //{
        //    var response = Utilities.SendDataRequest<Response>(ConstantValues.Authenticate.ResetPassword, model);

        //    if (response != null)
        //    {
        //        // Nếu đặt lại mật khẩu thành công
        //        return RedirectToAction("ResetPasswordSuccess");
        //    }
        //    else
        //    {
        //        // Nếu đặt lại mật khẩu không thành công
        //        return RedirectToAction("ResetPasswordFailure");
        //    }
        //}

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            var response = Utilities.SendDataRequest<Response>(ConstantValues.Authenticate.ChangePassword, model);
            if (response == null)
            {
                TempData["Errror"] = $"<h5>Errror</h5>" +
                            "Password changed failed!";
                return View();
            }
            TempData["SuccessMessage"] = "Successfully";
            return RedirectToAction("Index","Home");
        }
    }
}
