using System;
using System.Collections.Generic;
using Models;
using DataLogic;
using System.Linq;

namespace BuisnessLogic
{
    public class BLogic
    {
        private storeDB _DB;
        private int _CustID;
        private int _StoreID;
        public BLogic(storeDB DB)
        {
            _DB = DB;
        }

        public int CheckUserCredentials(string email, string password){
            return _DB.GetUserID(email, password);
        }

        public void FindUser(string name){
            Customer cust = _DB.FindCustomer(name);
            Console.WriteLine("Id: "+ cust.CustomerId);
            Console.WriteLine("Name: "+cust.Name);
            Console.WriteLine("Email: "+cust.Email);
        }
        public Customer FindUser(int customerId){
            return _DB.FindCustomer(customerId);
        }

        public Tuple<Order, OrderDetail> CheckOrder(int orderId){
            return _DB.ViewOrder(orderId);
        }

        public int AddNewUser(string name, string email, string password){
            _CustID = _DB.AddUser(name, email, password);
            if(!(_CustID > 0)) throw new Exception("Unable to verify user creation");
            return _CustID;
        }

        public Location FindLocation(string name){
            return _DB.FIndLocation(name);
        }

        public Location FindLocationById(int id)
        {
            return _DB.FindLocation(id);
        }

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

        public void ViewTransactionsByCustomer()
        {
            _DB.GetCustomerOrderAndDetails(_CustID);
        }

        public void ViewTransactionsByLocation(){
            _DB.TransactionByLocation(_StoreID);
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

        public void MakePurchase(int productId, int orderAmount)
        {
            try{
                _DB.SellItems(productId, orderAmount, _CustID);

            }catch(Exception e){
                Console.WriteLine("message: ");
                Console.WriteLine(e.InnerException.Message);
                string hold = Console.ReadLine();
            }
        }
 
    }
}