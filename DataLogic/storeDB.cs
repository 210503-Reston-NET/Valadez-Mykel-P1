using System;
using System.Linq;
using System.Collections.Generic;
using Models;

namespace DataLogic
{
    public class storeDB
    {
        private StoreDBContext _context;
        public storeDB(StoreDBContext context)
        {
            _context = context;
        }

        public int GetUserID(string email, string password){
            return _context.Customers.FirstOrDefault(cust => cust.Email.Equals(email) && cust.Password.Equals(password)).CustomerId;
        }

        public int AddUser(string name, string email, string password){
            _context.Customers.Add(new Customer{
                Name = name,
                Email = email,
                Password = password
            });
            _context.SaveChanges();
            return GetUserID(email, password);
        }
        public List<LocationProductInventory> ViewInventory(int locationId)
        {
            return _context.LocationProductInventories.Where(inv => inv.LocationId.Equals(locationId))
               .ToList();


            //int counter = 1;

            //_context.Locations.Join(_context.LocationProductInventories,
            //    loc => loc.LocationId,
            //    lpi => lpi.LocationId,
            //    (loc, lpi) =>
            //    new {
            //        Name = loc.Name,
            //        LocationId = loc.LocationId,
            //        Address = loc.Address,
            //        // Used to be able to pull the name out here with lpi.Product.Name
            //        ProductId = lpi.ProductId,
            //        Quantity = lpi.Quantity
            //    }
            //).Where(inv => inv.LocationId.Equals(locationId))
            //.ToList()
            //.ForEach(inv => {
            //    if(counter == 1){
            //        Console.WriteLine("Location Name: "+inv.Name);
            //        Console.WriteLine("Location Id: "+inv.LocationId);
            //        Console.WriteLine("Address: "+inv.Address);
            //        Console.WriteLine("");
            //    }
            //    Console.WriteLine("Product : "+inv.ProductId);
            //    Console.WriteLine("Quantity: "+inv.Quantity);
            //    Console.WriteLine();

            //    counter = 2;
            //});
        }

        public List<Order> TransactionByLocation(int locationId){
            return _context.Orders.Where(loc => loc.LocationId.Equals(locationId))
            .ToList();
            
        }

        public Tuple<Order, OrderDetail> ViewOrder(int orderId){

            //return _context.Orders.Join(_context.OrderDetails,
            //    ord => ord.OrderId,
            //    dets => dets.OrderId,
            //    (ord, dets) => 
            //    new {
            //        OrderId = ord.OrderId,
            //        CustomerId = ord.CustomerId,
            //        LocationId = ord.LocationId,
            //        ProductId = dets.ProductId,
            //        Quantity = dets.Quantity,
            //        Delivered = dets.Delivered
            //        }
            //    ).Where(ord => ord.OrderId.Equals(orderId));

            Order ord = _context.Orders.FirstOrDefault(ord => ord.OrderId.Equals(orderId));
            OrderDetail det = _context.OrderDetails.FirstOrDefault(ord => ord.OrderId.Equals(orderId));

            return Tuple.Create(ord, det);

        }

        public void AddLocation(string name, string address)
        {
            _context.Locations.Add(
                new Location{
                    Name = name,
                    Address = address
                }
            );
            _context.SaveChanges();
        }

        public Location FIndLocation(string name){
            return _context.Locations.First(loc => loc.Name.Equals(name));
        }

        public Location FindLocation(int id)
        {
            return _context.Locations.FirstOrDefault(loc => loc.LocationId.Equals(id));
        }

        public void AddInventory(int productId, int quantity, int locationId){
            
            IQueryable<LocationProductInventory> currentStock = _context.LocationProductInventories
            .Where(inv => inv.ProductId.Equals(productId) && inv.LocationId.Equals(locationId));
            
            if(currentStock.Count() == 0){

                _context.LocationProductInventories.Add(new LocationProductInventory{
                    LocationId = locationId,
                    ProductId = productId,
                    Quantity = quantity
                });
            }else{
                currentStock.ToList()[0].Quantity += quantity;
            }

            _context.SaveChanges();
        }

        public List<Location> GetAllLocations()
        {
            return _context.Locations.ToList();
        }

        public Customer FindCustomer(int customerId)
        {
            return _context.Customers.FirstOrDefault(cust => cust.CustomerId.Equals(customerId));
        }
        public Customer FindCustomer(string customerName)
        {
            return _context.Customers.FirstOrDefault(cust => cust.Name.Equals(customerName));
        }

        public int CheckItemAmount(int productId)
        {
            return _context.LocationProductInventories
            .Where(prod => prod.ProductId.Equals(productId))
            .Sum(s => s.Quantity);
        }

        public Product GetProductInfo(int productId)
        {
            return _context.Products.FirstOrDefault(prod => prod.ProductId.Equals(productId));
        }

        public void GetCustomerOrderAndDetails(int customerId){

            _context.Orders.Where(ord => ord.CustomerId.Equals(customerId))
            .ToList()
            .ForEach(ord => {

                _context.Orders.Join(_context.OrderDetails,
                ord => ord.OrderId,
                dets => dets.OrderId,
                (ord, dets) => 
                new {
                    OrderId = ord.OrderId,
                    CustomerId = ord.CustomerId,
                    LocationId = ord.LocationId,
                    ProductId = dets.ProductId,
                    Quantity = dets.Quantity,
                    Delivered = dets.Delivered
                    }
                ).ToList()
                .ForEach(row => {
                    Console.WriteLine("Order Id: "+row.OrderId);
                    Console.WriteLine("Customer Id: "+row.CustomerId);

                    Console.WriteLine("Location Id: "+row.LocationId);
                    Console.WriteLine("ProductId: "+row.ProductId);
                    Console.WriteLine("Quantity Ordered: "+row.Quantity);
                    Console.WriteLine("Delivered Yet?: "+row.Delivered);
                    Console.WriteLine("");
                });


            });
        }

        public void SellItems(int productId, int requestedQuantity, int customerId){
            int leftToBeSold = 0;
            int paritalRequestedQuantity = 0;

            do{
                List<LocationProductInventory> invWithProduct = _context.LocationProductInventories
                .Where(inv => inv.ProductId.Equals(productId)).ToList();

                int max = invWithProduct.Max(inv => inv.Quantity);

                if(max < requestedQuantity){
                    leftToBeSold = requestedQuantity - max;
                    paritalRequestedQuantity = max;
                }else{
                    paritalRequestedQuantity = requestedQuantity;
                }

                LocationProductInventory invent = invWithProduct.First(inv => inv.Quantity.Equals(max)); 
                invent.Quantity -= paritalRequestedQuantity;

                _context.SaveChanges();

                CreateOrder(productId, requestedQuantity, customerId, invent.LocationId);

            } while (leftToBeSold > 0);

            
            _context.ChangeTracker.Clear();
            
        }

        public void CreateOrder(int productId, int requestedQuantity, int customerId, int locationId){
            
            _context.Orders.Add(new Order{
                LocationId = locationId,
                CustomerId = customerId
            });

            _context.SaveChanges();

            int orderId = _context.Orders.Max(ord => ord.OrderId);

            _context.OrderDetails.Add(new OrderDetail{
                OrderId = orderId,
                ProductId = productId,
                Quantity = requestedQuantity,
            });

            _context.SaveChanges();

        }
    }
}