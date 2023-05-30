using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;
using X.PagedList;

namespace ToyStoreClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderAdminController : Controller
    {
        public IActionResult Index(int pageNo = 1)
        {
            var orders = Utilities.SendDataRequest<List<OrderModel>>(ConstantValues.Order.GetAllOrders);
            var pagedList = orders.ToPagedList(pageNo, 5);
            return View(pagedList);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var url = string.Format(ConstantValues.Order.GetOrderById, id);
            var order = Utilities.SendDataRequest<OrderModel>(url);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(Utilities.SendDataRequest<List<StatusModel>>(ConstantValues.Status.GetAllStatuses), "StatusId", "StatusName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderId,CustomerName,CustomerEmail,CustomerPhone,CustomerAddress,OrderDate,EstimatedDeliveryDate,StatusId")] OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                Utilities.SendDataRequest<OrderModel>(ConstantValues.Order.AddOrder, orderModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(Utilities.SendDataRequest<List<StatusModel>>(ConstantValues.Status.GetAllStatuses), "StatusId", "StatusName",orderModel.StatusId);
            return View(orderModel);
        }
  
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.Order.FindOrderById, id);
            var order = Utilities.SendDataRequest<OrderModel>(url);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(Utilities.SendDataRequest<List<StatusModel>>(ConstantValues.Status.GetAllStatuses), "StatusId", "StatusName", order.StatusId);
            return View(order);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("OrderId,CustomerName,CustomerEmail,CustomerPhone,CustomerAddress,OrderDate,EstimatedDeliveryDate,StatusId")] OrderModel order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Utilities.SendDataRequest<bool>(ConstantValues.Order.UpdateOrder);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["StatusId"] = new SelectList(Utilities.SendDataRequest<List<StatusModel>>(ConstantValues.Status.GetAllStatuses), "StatusId", "StatusName", order.StatusId);
            return View(order);
        }
   
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.Order.GetOrderById, id);
            var order = Utilities.SendDataRequest<OrderModel>(url);
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
            var url = string.Format(ConstantValues.Order.FindOrderById, id);
            var order = Utilities.SendDataRequest<OrderModel>(url);
            Utilities.SendDataRequest<bool>(ConstantValues.Order.DeleteOrder);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            var url = string.Format(ConstantValues.Order.ExistsById, id);
            var order = Utilities.SendDataRequest<bool>(url);
            if (order != true) return false;
            else return true;
        }
    }
}
