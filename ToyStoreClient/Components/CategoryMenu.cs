using Microsoft.AspNetCore.Mvc;
using ToyStoreClient.Helpers;
using ToyStoreClient.Models;

namespace ToyStoreClient.Components
{
    [ViewComponent(Name = "CategoryMenu")]
    public class CategoryMenu:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var categories = Utilities.SendDataRequest<List<CategoryModel>>(ConstantValues.Category.GetAllCategories);
            return View(categories);
        }
    }
}
