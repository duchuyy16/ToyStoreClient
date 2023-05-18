using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryAdminController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var categories = Utilities.SendDataRequest<List<CategoryModel>>(ConstantValues.Category.GetAllCategories);
                return View(categories.ToList());
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
        public IActionResult Create([Bind("CategoryId,CategoryName")] CategoryModel categoryModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<CategoryModel>(ConstantValues.Category.AddCategory, categoryModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(categoryModel);
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

                var url = string.Format(ConstantValues.Category.FindCategoryById, id);
                var category = Utilities.SendDataRequest<CategoryModel>(url);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CategoryId,CategoryName")] CategoryModel categoryModel)
        {
            try
            {
                if (id != categoryModel.CategoryId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<bool>(ConstantValues.Category.UpdateCategory, categoryModel);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CategoryExists(categoryModel.CategoryId))
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
                return View(categoryModel);
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
                var url = string.Format(ConstantValues.Category.GetCategoryById, id);

                var category = Utilities.SendDataRequest<CategoryModel>(url);

                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
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

                var url = string.Format(ConstantValues.Category.GetCategoryById, id);
                var category = Utilities.SendDataRequest<CategoryModel>(url);
                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
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
                var url = string.Format(ConstantValues.Category.FindCategoryById, id);
                var category = Utilities.SendDataRequest<CategoryModel>(url);

                Utilities.SendDataRequest<bool>(ConstantValues.Category.DeleteCategory, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();

            }
        }

        private bool CategoryExists(int id)
        {
            var url = string.Format(ConstantValues.Category.ExistsById, id);
            var category = Utilities.SendDataRequest<bool>(url);
            if (category != true) return false;
            else return true;
        }
    }
}
