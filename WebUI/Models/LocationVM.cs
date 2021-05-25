using System;
using Models;
namespace WebUI.Models
{
    public class LocationVM
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
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
