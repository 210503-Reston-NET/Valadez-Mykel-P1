using System.ComponentModel.DataAnnotations;
using Models;
namespace WebUI.Models
{
    public class LocationVM
    {
        public int LocationId { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", 
        ErrorMessage = "Enter in a valid name")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public LocationVM()
        {
        }

        public LocationVM(Location loc)
        {
            this.LocationId = loc.LocationId;
            this.Name = loc.Name;
            this.Address = loc.Address;
        }
    }
}
