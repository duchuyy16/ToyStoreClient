namespace ToyStoreClient.Helpers
{
    public class ConstantValues
    {
        public class Product
        {
            public const string GetAllProducts = "api/Products/GetAllProducts";
            public const string GetAllProductsBestSellers = "api/Products/GetAllProductsBestSellers";
            public const string GetProductById = "api/Products/GetProductById/{0}";
            public const string GetProductsByCategoryId = "api/Products/GetProductsByCategoryId/{0}";
            public const string SearchByProductName = "api/Products/SearchByProductName/{0}";
            public const string AddProduct = "api/Products/AddProduct";
            public const string UpdateProduct = "api/Products/UpdateProduct";
            public const string DeleteProduct = "api/Products/DeleteProduct";
            public const string FindProductById = "api/Products/FindProductById/{0}";
            public const string ExistsById = "api/Products/ExistsById/{0}";
            public const string CountProducts = "api/Products/CountProducts";
            public const string ProductStatistics = "api/Statistics/ProductStatistics";
            public const string Download = "api/Products/download";
        }
        public class Category
        {
            public const string GetCategoryById = "api/Categories/GetCategoryById/{0}";
            public const string GetAllCategories = "api/Categories/GetAllCategories";
            public const string AddCategory = "api/Categories/AddCategory";
            public const string UpdateCategory = "api/Categories/UpdateCategory";
            public const string DeleteCategory = "api/Categories/DeleteCategory";
            public const string FindCategoryById = "api/Categories/FindCategoryById/{0}";
            public const string ExistsById = "api/Categories/ExistsById/{0}";
            public const string CountCategories = "api/Categories/CountCategories";
        }

        public class User
        {
            public const string CountUsers = "api/Users/CountUsers";
            public const string GetUserById = "api/Users/GetUserById/{0}";
            public const string GetAllUsers = "api/Users/GetAllUsers";
        }

        public class Status
        {
            public const string GetAllStatuses = "api/Statuses/GetAllStatuses";
        }

        public class Order
        {
            public const string GetAllOrders = "api/Orders/GetAllOrders";
            public const string GetOrderById = "api/Orders/GetOrderById/{0}";
            public const string AddOrder = "api/Orders/AddOrder";
            public const string UpdateOrder = "api/Orders/UpdateOrder";
            public const string DeleteOrder = "api/Orders/DeleteOrder";
            public const string FindOrderById = "api/Orders/FindOrderById/{0}";
            public const string ExistsById = "api/Orders/ExistsById/{0}";
            public const string CountOrders = "api/Orders/CountOrders";
        }

        public class OrderDetail
        {
            public const string GetAllOrderDetails = "api/OrderDetails/GetAllOrderDetails";
            public const string GetOrderDetailById = "api/OrderDetails/GetOrderDetailById/{0}";
            public const string AddOrderDetail = "api/OrderDetails/AddOrderDetail";
            public const string UpdateOrderDetail = "api/OrderDetails/UpdateOrderDetail";
            public const string DeleteOrderDetail = "api/OrderDetails/DeleteOrderDetail";
            public const string ExistsById = "api/OrderDetails/ExistsById/{0}";
            public const string FindOrderDetailById = "api/OrderDetails/FindOrderDetailById/{0}";
        }

        public class Authenticate
        {
            public const string Login = "api/Authenticate/login";
            public const string Register = "api/Authenticate/register";
            public const string RegisterAdmin = "api/Authenticate/register-admin";
            public const string Logout = "api/Authenticate/logout";
            public const string ResetPasswordToken = "api/Authenticate/reset-password-token/{0}";
            public const string ResetPassword = "api/Authenticate/reset-password";
            public const string ChangePassword = "api/Authenticate/change-password";
        }

        public class Contact
        {
            public const string GetAllContacts = "api/Contacts/GetAllContacts";
            public const string AddContact = "api/Contacts/AddContact";
            public const string GetContactById = "api/Contacts/GetContactById/{0}";
            public const string UpdateContact = "api/Contacts/UpdateContact";
            public const string DeleteContact = "api/Contacts/DeleteContact";
            public const string FindContactById = "api/Contacts/FindContactById/{0}";
            public const string ExistsById = "api/Contacts/ExistsById/{0}";
        }

        public class Home
        {
            public const string Count = "api/Home/Count";
        }
    }
}
