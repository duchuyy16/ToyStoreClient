using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactAdminController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var contacts = Utilities.SendDataRequest<List<ContactModel>>(ConstantValues.Contact.GetAllContacts);
                return View(contacts.ToList());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var url = string.Format(ConstantValues.Contact.GetContactById, id);
                var contact = Utilities.SendDataRequest<ContactModel>(url);

                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ContactId,ContactName,Email,Message")] ContactModel contactModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<ContactModel>(ConstantValues.Contact.AddContact, contactModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(contactModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var url = string.Format(ConstantValues.Contact.FindContactById, id);
                var contact = Utilities.SendDataRequest<ContactModel>(url);
                if (contact == null)
                {
                    return NotFound();
                }
                return View(contact);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ContactId,ContactName,Email,Message,CreateAt")] ContactModel contactModel)
        {
            try
            {
                if (id != contactModel.ContactId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<bool>(ConstantValues.Contact.UpdateContact, contactModel);

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ContactExists(contactModel.ContactId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(contactModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Contact.GetContactById, id);
                var contact = Utilities.SendDataRequest<ContactModel>(url);
                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.Contact.FindContactById, id);
                var contact = Utilities.SendDataRequest<ContactModel>(url);

                Utilities.SendDataRequest<bool>(ConstantValues.Contact.DeleteContact, contact);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool ContactExists(int id)
        {
            var url = string.Format(ConstantValues.Contact.ExistsById, id);
            var contact = Utilities.SendDataRequest<bool>(url);
            if (contact != true) return false;
            else return true;
        }
    }
}
