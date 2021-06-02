using System;
using System.ComponentModel.DataAnnotations;
using Models;
namespace WebUI.Models
{
    public class OrderVM
    {
        [RegularExpression(@"^[0-9]+$",
        ErrorMessage = "Invalid order ID")]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$",
        ErrorMessage = "Invalid Quantity")]
        public int Quantity { get; set; }
        public bool? Delivered { get; set; }

        public int Available { get; set; }

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

        public OrderVM(int custid, int prodId){
            this.CustomerId = custid;
            this.ProductId = prodId;
        }

        public OrderVM()
        {

        }
    }
}
