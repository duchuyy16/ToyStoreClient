using Microsoft.AspNetCore.Mvc;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            var users = Utilities.SendDataRequest<List<AspNetUserModel>>(ConstantValues.User.GetAllUsers);
            return View(users.ToList());
        }

        public IActionResult Details(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var url = string.Format(ConstantValues.User.GetUserById, id);
                var user = Utilities.SendDataRequest<AspNetUserModel>(url);

                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

    }
}
