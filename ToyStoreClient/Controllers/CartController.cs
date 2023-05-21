using Microsoft.AspNetCore.Mvc;
using ToyStoreClient.Models;
using ToyStoreClient.Helpers;

namespace ToyStoreClient.Controllers
{
    public class CartController : Controller
    {
        public List<CartItem> Carts
        {
            get
            {
                var items = HttpContext.Session.Get<List<CartItem>>("cart");
                if (items == null)
                {
                    items = new List<CartItem>();
                }
                return items;
            }
        }

        public IActionResult AddToCart(int id, int quantity)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.ProductId == id);
            if (item == null)
            {
                var url = string.Format(ConstantValues.Product.GetProductById, id);
                var product = Utilities.SendDataRequest<ProductModel>(url);
                item = new CartItem
                {
                    ProductId = id,
                    ProductName = product.ProductName,
                    Image = product.Image,
                    Price = (decimal)(product.Price - (product.Price * product.Discount / 100))!,
                    Discount = product.Discount,
                    Quantity = quantity,

                };
                myCart.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
            HttpContext.Session.Set("cart", myCart);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateCartItem(int id, int quantity)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("cart");
            if (cart != null)
            {
                var cartItem = cart.FirstOrDefault(i => i.ProductId == id);
                if (cartItem != null)
                {
                    cartItem.Quantity = quantity;
                    HttpContext.Session.Set("cart", cart);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            HttpContext.Session.Set("cart", Carts.Where(p => p.ProductId != id).ToList());
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            ViewData["Cart"] = HttpContext.Session.Get<List<CartItem>>("cart");
            return View(Carts);
        }

        public IActionResult SaveOrder(string first, string last, string street, string town, string phone, string email)
        {

            OrderModel order = new OrderModel();
            order.CustomerName = first + " " + last;
            order.CustomerAddress = street + " " + town;
            order.CustomerEmail = email;
            order.CustomerPhone = phone;
            order.OrderDate = DateTime.Now.Date;
            order.EstimatedDeliveryDate = DateTime.Now.AddDays(3);
            order.StatusId = 1;
            order.Status = new StatusModel{ StatusId = 1, StatusName = "..." };

            var addOrder = Utilities.SendDataRequest<OrderModel>(ConstantValues.Order.AddOrder, order);

            List<CartItem> lstCart = HttpContext.Session.Get<List<CartItem>>("cart")!;

            for (int i = 0; i < lstCart.Count(); i++)
            {
                OrderDetailModel orderDetail = new OrderDetailModel
                {
                    OrderId = addOrder.OrderId,
                    ProductId = lstCart[i].ProductId,
                    Quantity = lstCart[i].Quantity,
                    Price = lstCart[i].Price,
                    Discount = lstCart[i].Discount,
                };
                Utilities.SendDataRequest<OrderDetailModel>(ConstantValues.OrderDetail.AddOrderDetail, orderDetail);
            }

            HttpContext.Session.Remove("cart");

            TempData["State"] = "Success";
            TempData["Message"] = "Order has confirmed we've also sent an email for confirmation. It will be deliverd soon thank you for shopping.";


            return RedirectToAction("Index", "Home");
        }

        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
