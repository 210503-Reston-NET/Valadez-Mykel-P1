namespace Models
{
    public class OrderDetail
    {
        public int OrderDetailsId { get; set; }
        public int Quantity { get; set; }
        public bool Delivered { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public OrderDetail(){
            Delivered = false;

        }
        public OrderDetail(int quantity, bool delivered, int productId, int orderId){
            this.Quantity = quantity;
            this.Delivered = delivered;
            this.ProductId = productId;
            this.OrderId = orderId;

            Delivered = false;
        }
    }
}