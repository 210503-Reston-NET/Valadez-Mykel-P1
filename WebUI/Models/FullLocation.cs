using System;
using Models;
using System.Collections.Generic;
namespace WebUI.Models
{
    public class FullLocation
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int LocationId { get; set; }
        public List<LocationProductInventory> Items{ get; set;}

        public FullLocation(Location location, List<LocationProductInventory> items)
        {
            this.Name = location.Name;
            this.Address = location.Address;
            this.LocationId = location.LocationId;
            this.Items = items;
        }

        public FullLocation(int id)
        {
            this.LocationId = id;
        }

        public FullLocation()
        {

        }
    }
}
