using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;
using X.PagedList;

namespace ToyStoreClient.Areas.Admin.Controllers
{
    public class OrderDetailAdminController : Controller
    {
        public IActionResult Index(int pageNo = 1)
        {
            var orderDetails = Utilities.SendDataRequest<List<OrderDetailModel>>(ConstantValues.OrderDetail.GetAllOrderDetails);
            var pagedList = orderDetails.ToPagedList(pageNo, 5);
            return View(pagedList);


        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var url = string.Format(ConstantValues.OrderDetail.GetOrderDetailById, id);
            var orderDetail = Utilities.SendDataRequest<OrderDetailModel>(url);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(Utilities.SendDataRequest<List<OrderModel>>(ConstantValues.Order.GetAllOrders), "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProducts), "ProductId", "ProductName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderDetailId,OrderId,ProductId,Quantity,Price,Discount")] OrderDetailModel orderDetailModel)
        {
            if (ModelState.IsValid)
            {
                Utilities.SendDataRequest<OrderDetailModel>(ConstantValues.OrderDetail.AddOrderDetail, orderDetailModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(Utilities.SendDataRequest<List<OrderModel>>(ConstantValues.Order.GetAllOrders), "OrderId", "OrderId", orderDetailModel.OrderId);
            ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProducts), "ProductId", "ProductName",orderDetailModel.ProductId);
            return View(orderDetailModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.OrderDetail.FindOrderDetailById, id);
            var orderDetail = Utilities.SendDataRequest<OrderDetailModel>(url);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(Utilities.SendDataRequest<List<OrderModel>>(ConstantValues.Order.GetAllOrders), "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProducts), "ProductId", "ProductName", orderDetail.ProductId);
            return View(orderDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("OrderDetailId,OrderId,ProductId,Quantity,Price,Discount")] OrderDetailModel orderDetailModel)
        {
            if (id != orderDetailModel.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Utilities.SendDataRequest<bool>(ConstantValues.OrderDetail.UpdateOrderDetail);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetailModel.OrderId))
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
            ViewData["OrderId"] = new SelectList(Utilities.SendDataRequest<List<OrderModel>>(ConstantValues.Order.GetAllOrders), "OrderId", "OrderId", orderDetailModel.OrderId);
            ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<ProductModel>>(ConstantValues.Product.GetAllProducts), "ProductId", "ProductName", orderDetailModel.ProductId);
            return View(orderDetailModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.OrderDetail.GetOrderDetailById, id);
            var order = Utilities.SendDataRequest<OrderDetailModel>(url);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var url = string.Format(ConstantValues.OrderDetail.FindOrderDetailById, id);
            var order = Utilities.SendDataRequest<OrderDetailModel>(url);
            Utilities.SendDataRequest<bool>(ConstantValues.OrderDetail.DeleteOrderDetail);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            var url = string.Format(ConstantValues.OrderDetail.ExistsById, id);
            var order = Utilities.SendDataRequest<bool>(url);
            if (order != true) return false;
            else return true;
        }
    }
}
