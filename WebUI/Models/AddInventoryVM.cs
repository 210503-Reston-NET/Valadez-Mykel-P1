using System;
using System.ComponentModel.DataAnnotations;
namespace WebUI.Models
{
    public class AddInventoryVM
    {
        [Required]
        [Range(1, 4,
        ErrorMessage = "Must be a valid Product ID (1-4)")]
        public int ProductId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", 
         ErrorMessage = "Must input a number")]
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
