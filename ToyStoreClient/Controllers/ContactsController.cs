using Microsoft.AspNetCore.Mvc;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Controllers
{
    public class ContactsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new ContactModel();
            return View(model);
        }
        [HttpGet]
        public IActionResult Contact()
        {
            var model = new ContactModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Contact(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Utilities.SendDataRequest<ContactModel>(ConstantValues.Contact.AddContact, model);

            return RedirectToAction("Index");
        }
    }
}
