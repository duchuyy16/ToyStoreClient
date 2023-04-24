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
            //public const string ThongKeSanPhamTheoTheLoai = "api/ThongKe/ThongKeSanPhamTheoTheLoai";
        }
        public class User
        {
            public const string DanhSachKhachHang = "api/Customer/DanhSachKhachHang";
            public const string ChiTietKhachHang = "api/Customer/ChiTietKhachHang/{0}";
            public const string ThemKhachHang = "api/Customer/ThemKhachHang";
            public const string CapNhatKhachHang = "api/Customer/CapNhatKhachHang";
            public const string XoaKhachHang = "api/Customer/XoaKhachHang";
            public const string CustomerExists = "api/Customer/CustomerExists/{0}";
            public const string KiemTraUsername = "api/Customer/KiemTraUsername/{0}";
            public const string TimKiem = "api/Customer/TimKiem/{0}";
            public const string DangNhap = "api/Customer/DangNhap";
            public const string DangKy = "api/Customer/DangKy";
        }
        public class Status
        {
            public const string DanhSachNhanHieu = "api/Statuses/DanhSachNhanHieu";
            public const string ChiTietNhanHieu = "api/Statuses/ChiTietNhanHieu/{0}";
            public const string ThemNhanHieu = "api/Statuses/ThemNhanHieu";
            public const string CapNhatNhanHieu = "api/Statuses/CapNhatNhanHieu";
            public const string XoaNhanHieu = "api/Statuses/XoaNhanHieu";
            public const string TimKiem = "api/Statuses/TimKiem/{0}"; 
            public const string ExistsById = "api/Statuses/BrandExists/{0}";
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
        public class Role
        {
            public const string DanhSachQuyen = "api/Role/DanhSachQuyen";
            public const string ChiTietQuyen = "api/Role/ChiTietQuyen/{0}";
            public const string ThemQuyen = "api/Role/ThemQuyen";
            public const string CapNhatQuyen = "api/Role/CapNhatQuyen";
            public const string XoaQuyen = "api/Role/XoaQuyen";
            public const string TimKiem = "api/Role/TimKiem/{0}";
            public const string ExistsById = "api/Role/ExistsById/{0}";
        }
        public class Contact
        {
            public const string AddContact = "api/Contacts/AddContact";
            //public const string DanhSachLienLac = "api/Contact/DanhSachLienLac";
            //public const string ChiTietLienLac = "api/Contact/ChiTiet/{0}";
            //public const string CapNhatLienLac = "api/Contact/CapNhatLienLac";
            //public const string XoaLienLac = "api/Contact/XoaLienLac";
            //public const string TimKiem = "api/Contact/TimKiem/{0}";
            //public const string ExistsById = "api/Contact/ExistsById/{0}";
        }
    }
}
