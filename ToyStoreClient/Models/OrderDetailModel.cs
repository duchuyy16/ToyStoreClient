namespace ToyStoreClient.Models
{
    public class OrderDetailModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public OrderModel Order { get; set; } = null!;
        public ProductModel Product { get; set; } = null!;
    }
}
