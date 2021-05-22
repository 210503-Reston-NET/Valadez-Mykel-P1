namespace Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public Order(){

        }
        public Order(int locationId, int customerId){
            this.LocationId = locationId;
            this.CustomerId = customerId;
        }
    }
}