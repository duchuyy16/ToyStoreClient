using ToyStoreClient.Models;

namespace ToyStoreClient.Services.Interfaces
{
    public interface ICart
    {
        int CountCart();
        List<CartItem> GetAllCarts();
       
    }
}
