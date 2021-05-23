using System;
using System.Collections.Generic;
using Models;
using DataLogic;

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
        public void FindUser(int customerId){
            Customer cust = _DB.FindCustomer(customerId);
            Console.WriteLine("Id: "+ cust.CustomerId);
            Console.WriteLine("Name: "+cust.Name);
            Console.WriteLine("Email: "+cust.Email);
        }

        public void CheckOrder(int orderId){
            _DB.ViewOrder(orderId);
        }

        public int AddNewUser(string name, string email, string password){
            _CustID = _DB.AddUser(name, email, password);
            if(!(_CustID > 0)) throw new Exception("Unable to verify user creation");
            return _CustID;
        }

        public void FindLocationByName(string name){
            _StoreID = _DB.FIndLocation(name).LocationId;
        }

        public void ViewInventory()
        {
            _DB.ViewInventory(_StoreID);
        }

        public void AddInventory(int productId, int quantity)
        {
            _DB.AddInventory(productId, quantity, _StoreID);
            Console.Clear();
            ViewInventory();
        }

        public void AddLocation(string name, string address){
            _DB.AddLocation(name, address);
        }

        public void ViewTransactionsByCustomer()
        {
            _DB.GetCustomerOrderAndDetails(_CustID);
        }

        public void ViewTransactionsByLocation(){
            _DB.TransactionByLocation(_StoreID);
        }

        public void GetAllStores()
        {
            Console.Clear();
            _DB.GetAllLocations().ForEach(i => Console.WriteLine(i.Name+" Location \nAddress:\n"+i.Address+"\n"));
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