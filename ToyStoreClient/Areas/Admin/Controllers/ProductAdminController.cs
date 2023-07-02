using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;
using X.PagedList;

namespace ToyStoreClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductAdminController : Controller
    {
        //public IActionResult ExportToExcel()
        //{
        //    var productList = Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProducts);

        //    using (var package = new ExcelPackage())
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("ListOfProducts");
        //        // Thêm tiêu đề cho các cột trong bảng
        //        worksheet.Cells[1, 1].Value = "ID sản phẩm";
        //        worksheet.Cells[1, 2].Value = "Tên sản phẩm";
        //        worksheet.Cells[1, 3].Value = "Gía";
        //        worksheet.Cells[1, 4].Value = "Mẫu năm";
        //        worksheet.Cells[1, 5].Value = "Giảm giá";
        //        worksheet.Cells[1, 6].Value = "Mô tả";
        //        worksheet.Cells[1, 7].Value = "Hình Ảnh";
        //        worksheet.Cells[1, 8].Value = "Thể Loại";
        //        // Thêm dữ liệu cho từng sản phẩm
        //        for (int i = 0; i < productList.Count; i++)
        //        {
        //            worksheet.Cells[i + 2, 1].Value = productList[i].ProductId;
        //            worksheet.Cells[i + 2, 2].Value = productList[i].ProductName;
        //            worksheet.Cells[i + 2, 3].Value = productList[i].Price;
        //            worksheet.Cells[i + 2, 4].Value = productList[i].ModelYear;
        //            worksheet.Cells[i + 2, 5].Value = productList[i].Discount;
        //            worksheet.Cells[i + 2, 6].Value = productList[i].Description;
        //            worksheet.Cells[i + 2, 7].Value = productList[i].Image;
        //            worksheet.Cells[i + 2, 8].Value = productList[i].CategoryId;
        //        }

        //        // Tự động điều chỉnh cỡ cột để hiển thị đầy đủ dữ liệu
        //        worksheet.Cells.AutoFitColumns();

        //        // Lưu tệp Excel vào bộ nhớ đệm
        //        var stream = new MemoryStream();
        //        package.SaveAs(stream);

        //        // Trả về tệp Excel dưới dạng phản hồi HTTP
        //        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", @"D:\ToyStore\Excel\ListOfProducts.xlsx");
        //    }
        //}

        public IActionResult ExportToExcel()
        {
            var products = Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.Download);
            return View(products);
        }

        public IActionResult Index(int? categoryId, int pageNo = 1)
        {
            try
            {
                var categories = Utilities.SendDataRequest<List<CategoryModel>>(ConstantValues.Category.GetAllCategories);
                categories.Insert(0, new CategoryModel { CategoryId = 0, CategoryName = "----------Select Category----------" });
                ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", categoryId);
                if (categoryId == null)
                {
                    var products = Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProducts);
                    var pagedList = products.ToPagedList(pageNo, 5);
                    return View(pagedList);
                }
                else
                {
                    var products = Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProducts);
                    var pagedList = products.Where(x => x.CategoryId == categoryId).ToPagedList(pageNo, 5);
                    return View(pagedList);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public IActionResult SearchByName(string name,int? categoryId, int pageNo=1)
        {
            try
            {
                var categories = Utilities.SendDataRequest<List<CategoryModel>>(ConstantValues.Category.GetAllCategories);
                categories.Insert(0, new CategoryModel { CategoryId = 0, CategoryName = "Please select a category" });
                ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", categoryId);
                if (categoryId == null)
                {
                    var url = string.Format(ConstantValues.Product.SearchByProductName, name);
                    var products = Utilities.SendDataRequest<List<ProductModel>>(url);
                    var pagedList = products.ToPagedList(pageNo, 5);
                    return View(pagedList);
                }
                else
                {
                    var url = string.Format(ConstantValues.Product.SearchByProductName, name);
                    var products = Utilities.SendDataRequest<List<ProductModel>>(url);
                    var pagedList = products.Where(x => x.CategoryId == categoryId).ToPagedList(pageNo, 5);
                    return View(pagedList);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: Admin/ProductAdmin/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["CategoryId"] = new SelectList(Utilities.SendDataRequest<List<CategoryModel>>(ConstantValues.Category.GetAllCategories), "CategoryId", "CategoryName");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // POST: Admin/ProductAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,ProductName,Price,ModelYear,Discount,Description,Image,CategoryId")] ProductModel productModel/*,IFormFile imageFile*/)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Upload(productModel, imageFile);
                    Utilities.SendDataRequest<ProductModel>(ConstantValues.Product.AddProduct, productModel);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CategoryId"] = new SelectList(Utilities.SendDataRequest<List<CategoryModel>>(ConstantValues.Category.GetAllCategories), productModel.CategoryId);
                return View(productModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: Admin/ProductAdmin/Edit/5
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Product.GetProductById, id);
                var product = Utilities.SendDataRequest<ProductModel>(url);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
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

                var url = string.Format(ConstantValues.Product.GetProductById, id);
                var product = Utilities.SendDataRequest<ProductModel>(url);
                if (product == null)
                {
                    return NotFound();
                }

                ViewData["CategoryId"] = new SelectList(Utilities.SendDataRequest<List<CategoryModel>>(ConstantValues.Category.GetAllCategories), "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        //public string GetDataPath(string file) => $"\\{file}";
        //public void Upload(ProductModel productModel, IFormFile file)
        //{
        //    if (file != null)
        //    {
        //        var path = GetDataPath(file.FileName);
        //        using var stream = new FileStream(path, FileMode.Create);
        //        file.CopyTo(stream);
        //        productModel.Image = file.FileName;
        //    }
        //}
        //POST: Admin/ProductModel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,ProductName,Price,ModelYear,Discount,Description,Image,CategoryId")] ProductModel productModel,IFormFile imageFile)
        {
            try
            {
                if (id != productModel.ProductId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        //productModel.Category = new CategoryModel
                        //{
                        //    CategoryId = 1,
                        //    CategoryName = "test"
                        //};
                        //Upload(productModel, imageFile);
                        Utilities.FromData<bool>(ConstantValues.Product.UpdateProduct, productModel,imageFile);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(productModel.ProductId))
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
                
                ViewData["CategoryId"] = new SelectList(Utilities.SendDataRequest<List<CategoryModel>>(ConstantValues.Category.GetAllCategories), "CategoryId", "CategoryName", productModel.CategoryId);
                return View(productModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: Admin/ProductModel/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Product.GetProductById, id);
                var product = Utilities.SendDataRequest<ProductModel>(url);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // POST: Admin/ProductModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.Product.GetProductById, id);
                var product = Utilities.SendDataRequest<ProductModel>(url);
                Utilities.SendDataRequest<bool>(ConstantValues.Product.DeleteProduct, product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool ProductExists(int id)
        {
            var url = string.Format(ConstantValues.Product.ExistsById, id);
            var product = Utilities.SendDataRequest<bool>(url);
            if (product != true) return false;
            else return true;
        }
    }
}
