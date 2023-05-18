using ToyStoreClient.Models;
using ToyStoreClient.Services.Interfaces;
using ToyStoreClient.Helpers;

namespace ToyStoreClient.Services
{
    public class Cart : ICart
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public Cart(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public int CountCart()
        {
            List<CartItem> lstCart = _contextAccessor.HttpContext!.Session.Get<List<CartItem>>("cart")!;

            if(lstCart==null)
                return 0;

            return lstCart.Count();
        }

        public List<CartItem> GetAllCarts()
        {
            List<CartItem> lstCart = _contextAccessor.HttpContext!.Session.Get<List<CartItem>>("cart")!;
            if(lstCart ==null)
                return new List<CartItem>();
            return lstCart;
        }
    }
}
