namespace Models
{
    public class LocationProductInventory
    {
        
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public LocationProductInventory(){

        }
        public LocationProductInventory(int quantity, int productId, int locationId){
            this.Quantity = quantity;
            this.ProductId = productId;
            this.LocationId = locationId;
        }
    }
}