using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;
using X.PagedList;

namespace ToyStoreClient.Controllers
{
    public class ProductsController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult SearchByProductName(string keyword)
        {
            ViewBag.Name = keyword;
            if (!string.IsNullOrEmpty(keyword))
            {
                var url = string.Format(ConstantValues.Product.SearchByProductName, keyword);
                var products = Utilities.SendDataRequest<List<ProductModel>>(url);
                return View(products);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult GetAllProducts(int? categoryId, int pageNo = 1)
        {
            try
            {
                var categories = Utilities.SendDataRequest<List<CategoryModel>>(ConstantValues.Category.GetAllCategories);
                categories.Insert(0, new CategoryModel { CategoryId = 0, CategoryName = "Please select a category" });
                ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", categoryId);
                if (categoryId == null)
                {
                    var products = Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProducts);
                    var pagedList = products.ToPagedList(pageNo, 8);
                    return View(pagedList);
                }
                else
                {
                    var products = Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProducts);
                    var pagedList = products.Where(x => x.CategoryId == categoryId).ToPagedList(pageNo, 8);
                    return View(pagedList);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        public IActionResult GetProductDetails(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.Product.GetProductById, id);
                var productDetail = Utilities.SendDataRequest<ProductModel>(url);

                var products = Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProductsBestSellers).Take(4);

                ViewBag.Products=products;

                if (productDetail == null)
                {
                    return RedirectToAction("Index");
                }

                return View(productDetail);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult GetProductCategory(int categoryId)
        {
            try
            {
                var url = string.Format(ConstantValues.Product.GetProductsByCategoryId, categoryId);
                var products = Utilities.SendDataRequest<List<ProductModel>>(url);
                if (products == null)
                {
                    return RedirectToAction("Index");
                }
                return View(products);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
