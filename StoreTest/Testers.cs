using DataLogic;
using Xunit;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StoreTest
{
    /// <summary>
    /// Test Class for Database 
    /// </summary>
    public class Testers
    {
        private readonly DbContextOptions<StoreDBContext> options;

        public Testers()
        {

            options = new DbContextOptionsBuilder<DataLogic.StoreDBContext>()
            .UseSqlite("Filename=Test.db").Options;
            
            Seed();
        }
        //Test Read Ops
        //When testing methods that do not state of the data in the db only one context instance is needed
        //What methods does not affect db state: Read
        [Fact]
        public void GetLocation()
        {
            //putting in a test context/ connection to our test db
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                Location location = _repo.FIndLocation("Scranton");

                //Assert
                Assert.Equal(1, location.LocationId);
            }
        }
        
        [Fact]
        public void GetOrderByCustomer()
        {
            //putting in a test context/ connection to our test db
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                List<Tuple<Order, OrderDetail>> orders = _repo.GetCustomerOrderAndDetails(1);

                //Assert
                Assert.Equal(1, orders[0].Item1.OrderId);
                Assert.Equal(44, orders[0].Item2.Quantity);
            }
        }
        [Fact]
        public void FindCustomer()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                Customer customer = _repo.FindCustomer(1);

                //Assert
                Assert.Equal(1, customer.CustomerId);
            }
        }
        [Fact]
        public void GetUserId()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                int userId = _repo.GetUserID("valadezmykel@gmail.com", "password");

                //Assert
                Assert.Equal(1, userId);
            }
        }
        [Fact]
        public void ViewInventory()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                List<LocationProductInventory> inventory = _repo.ViewInventory(1);

                //Assert
                Assert.Equal(200, inventory[0].Quantity);
                Assert.Equal(1, inventory[0].ProductId);
            }
        }
        [Fact]
        public void TransactionByLocation()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                List<Order> orders = _repo.TransactionByLocation(1);

                //Assert
                Assert.Equal(1, orders[0].OrderId);
                Assert.Equal(1, orders[0].CustomerId);
            }
        }
        [Fact]
        public void ViewOrder()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                Tuple<Order, OrderDetail> order = _repo.ViewOrder(1);

                //Assert
                Assert.Equal(1, order.Item1.CustomerId);
                Assert.Equal(1, order.Item1.LocationId);
                Assert.Equal(1, order.Item2.ProductId);
            }
        }
        [Fact]
        public void AddLocation()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                _repo.AddLocation("Concord", "343434");
                Location loc = _repo.FIndLocation("Concord");
                //Assert
                Assert.Equal(2, loc.LocationId);
            }
        }
        [Fact]
        public void FindLocation()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                Location loc = _repo.FindLocation(1);

                //Assert
                Assert.Equal(1, loc.LocationId);
            }
        }
        [Fact]
        public void GetAllLocations()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                List<Location> locations = _repo.GetAllLocations();

                //Assert
                Assert.Equal(1, locations[0].LocationId);
            }
        }
        [Fact]
        public void FindCustomerById()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                Customer user = _repo.FindCustomer(1);

                //Assert
                Assert.Equal("Mykel", user.Name);
            }
        }
        [Fact]
        public void FindCustomerByname()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                Customer user = _repo.FindCustomer("Mykel");

                //Assert
                Assert.Equal( 1, user.CustomerId);
            }
        }
        [Fact]
        public void CheckItemAmount()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                int available = _repo.CheckItemAmount(1);

                //Assert
                Assert.Equal(200, available);
            }
        }
        [Fact]
        public void GetProductInfo()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                Product prod = _repo.GetProductInfo(1);

                //Assert
                Assert.Equal(5.99, prod.Price);
            }
        }
        [Fact]
        public void AddUser()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                //Arrange
                DataLogic.storeDB _repo = new DataLogic.storeDB(context);

                //Act
                int userId = _repo.AddUser("Billy", "va@gmail.com", "word");

                //Assert
                Assert.Equal(2, userId);
            }
        }
        [Fact]
        public void CustomerShouldTakeValidData()
        {
            // Arrange
            string name = "Steph";
            Customer test = new Customer();

            // Act
            test.Name = name;

            // Assert
            Assert.Equal(name, test.Name);
        }
        [Fact]
        public void LocationShouldTakeValidData()
        {
            // Arrange
            string name = "Detroit";
            Location test = new Location();

            // Act
            test.Name = name;

            // Assert
            Assert.Equal(name, test.Name);
        }
        [Fact]
        public void LocationProductInventoryShouldTakeValidData()
        {
            // Arrange
            int quantity = 44;
            LocationProductInventory test = new LocationProductInventory();

            // Act
            test.Quantity = quantity;

            // Assert
            Assert.Equal(quantity, test.Quantity);
        }
        [Fact]
        public void OrderShouldTakeValidData()
        {
            // Arrange
            int locationId = 1;
            Order test = new Order();

            // Act
            test.LocationId = locationId;

            // Assert
            Assert.Equal(locationId, test.LocationId);
        }
        [Fact]
        public void ProductShouldTakeValidData()
        {
            // Arrange
            string name = "Rocks";
            Product test = new Product();

            // Act
            test.Name = name;

            // Assert
            Assert.Equal(name, test.Name);
        }
        //When testing operations that change the state of the db (i.e manipulate the data inside the db) 
        //make sure to check if the change has persisted even when accessing the db using a different context/connection
        //This means that you create another instance of your context when testing to check that the method has 
        //definitely affected the db.
        //What operations affect the state of the db? Create, Update, Delete
        private void Seed()
        {
            using (var context = new DataLogic.StoreDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Locations.Add(
                    new Location{
                        LocationId = 1,
                        Name = "Scranton",
                        Address = "5354 West Pickle St. Scranton, OH 99849"
                    }
                
                );
                context.Products.Add(
                    new Product{
                        ProductId = 1,
                        Name = "Dirt",
                        Price = 5.99
                    }
                );
                context.Customers.Add(
                    new Customer{
                        Name = "Mykel",
                        CustomerId = 1,
                        Email = "valadezmykel@gmail.com",
                        Password = "password"
                    }
                );
                context.Orders.Add(
                    new Order{
                        LocationId = 1,
                        CustomerId = 1,
                        OrderId = 1
                    }
                );
                context.OrderDetails.Add(
                    new OrderDetail{
                        Quantity = 44,
                        Delivered = false,
                        ProductId = 1,
                        OrderId = 1,
                    }
                );
                context.LocationProductInventories.Add(
                    new LocationProductInventory{
                        Quantity = 200,
                        ProductId = 1,
                        LocationId = 1
                    }
                );
                context.SaveChanges();
            }
        }

    }
}