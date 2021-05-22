using System;

namespace Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Location(){

        }
        public Location(string name, string address){
            this.Name = name;
            this.Address = address;
        }

        public Location(int id, string name, string address): this(name, address){
            this.LocationId = id;
        }
    }
}