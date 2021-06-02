using System;
using System.Collections.Generic;
using Models;
using DataLogic;
using System.Linq;
using Serilog;

namespace BuisnessLogic
{
    public class BLogic
    {
        private storeDB _DB;
        private int _CustID;
        public BLogic(storeDB DB)
        {
            _DB = DB;
        }
        /// <summary>
        /// This will check if a user exists in the DB
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Customer ID</returns>
        public int CheckUserCredentials(string email, string password){
            Log.Information("retriving user");
            Log.Debug("DebugLog");
            
            return _DB.GetUserID(email, password);
        }

        public void FindUser(string name){
            Customer cust = _DB.FindCustomer(name);
        }
        /// <summary>
        /// Finds a Customer by id from the db
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>A Customer</returns>
        public Customer FindUser(int customerId){
            return _DB.FindCustomer(customerId);
        }
        /// <summary>
        /// This finds 1 order and order detail by id to be used to create an OrderVM
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Tuple of order and orderdetail</returns>
        public Tuple<Order, OrderDetail> CheckOrder(int orderId){
            return _DB.ViewOrder(orderId);
        }
        /// <summary>
        /// Adds a new user to the DB
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Customer ID</returns>
        public int AddNewUser(string name, string email, string password){
            _CustID = _DB.AddUser(name, email, password);
            if(!(_CustID > 0)) throw new Exception("Unable to verify user creation");
            return _CustID;
        }
        /// <summary>
        /// Finds a Location by name from the db
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A Location</returns>
        public Location FindLocation(string name){
            return _DB.FIndLocation(name);
        }
        /// <summary>
        /// Finds a Location by id from the db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A location</returns>
        public Location FindLocationById(int id)
        {
            return _DB.FindLocation(id);
        }
        /// <summary>
        /// Asks DB to find all Inventories with the same store id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of LocationProductInventories</returns>
        public List<LocationProductInventory> ViewInventory(int id)
        {
            return _DB.ViewInventory(id);
        }

        public void AddInventory(int productId, int quantity, int locationId)
        {
            _DB.AddInventory(productId, quantity, locationId);
        }

        public void AddLocation(Location loc){
            _DB.AddLocation(loc.Name, loc.Address);
        }

        public List<Tuple<Order, OrderDetail>> ViewTransactionsByCustomer(int id)
        {
            return _DB.GetCustomerOrderAndDetails(id);
        }

        public List<Order> ViewTransactionsByLocation(int id){
            return _DB.TransactionByLocation(id);
        }

        public List<Location> GetAllStores()
        {
            return _DB.GetAllLocations();
        }

        public int CheckItemAmount(int productId)
        {
            return _DB.CheckItemAmount(productId);
        }

        public double GetProductPrice(int productId)
        {
            return _DB.GetProductInfo(productId).Price;
        }

        public void MakePurchase(int productId, int orderAmount, int custId)
        {
            try{
                _DB.SellItems(productId, orderAmount, custId);

            }catch(Exception e){
                Console.WriteLine("message: ");
                Console.WriteLine(e.InnerException.Message);
                string hold = Console.ReadLine();
            }
        }
 
    }
}