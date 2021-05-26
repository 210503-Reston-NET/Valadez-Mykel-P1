using System;
namespace WebUI.Models
{
    public class AddInventoryVM
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int LocationId { get; set; }

        public AddInventoryVM(int productId, int quantity, int locationId)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
            this.LocationId = locationId;
        }

        public AddInventoryVM(int locationId)
        {
            this.LocationId = locationId;
        }

        public AddInventoryVM()
        {

        }
    }
}
