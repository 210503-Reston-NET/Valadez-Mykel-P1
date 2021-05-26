using System;
using System.Linq;
using Models;
namespace WebUI.Models
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool? Delivered { get; set; }

        public OrderVM(Tuple<Order, OrderDetail> ord)
        {
            this.OrderId = ord.Item1.OrderId;
            this.CustomerId = ord.Item1.CustomerId;
            this.LocationId = ord.Item1.LocationId;
            this.ProductId = ord.Item2.ProductId;
            this.Quantity = ord.Item2.Quantity;
            this.Delivered = ord.Item2.Delivered;
        }

        public OrderVM(int orderid)
        {
            this.OrderId = orderid;
        }

        public OrderVM(CustomerVM custid)
        {
            this.CustomerId = custid.CustomerId;
        }

        public OrderVM()
        {

        }
    }
}
