using System;
using Models;
using DataLogic;
using BuisnessLogic;

namespace UI.Menus
{
    public class CustMainMenu
    {
        public void Start(BLogic BL)
        {
            Console.Clear();
            Console.WriteLine("Welcome to JoeyBob's Dirt Super Supply");
            Console.WriteLine("What chew wan ?");
            Console.WriteLine();
            Console.WriteLine("[0] Order Online");
            Console.WriteLine("[1] Find a Store");
            Console.WriteLine("[2] Check on yer Orders");
            Console.WriteLine("[3] Log Out");

            string input = Console.ReadLine();

            bool run = true;
            while(run){
                switch (input)
                {
                    case "0":
                        OrderOnline(BL);
                        break;
                    case "1":
                        Nearestlocation(BL);
                        break;
                    case "2":
                        CheckOrders(BL);
                        break;
                    case "3":
                        new LoginMenu().Start();
                        break;
                    default:
                        Console.WriteLine("Invalid Input"); 
                        break;
                }
            }
        }

        public void OrderOnline(BLogic BL)
        {
            bool viewing = true;
            
            Console.Clear();
            Console.WriteLine("Welcome to JoeyBob's Dirt Super Supply");
            Console.WriteLine("What chew wan ?");
            Console.WriteLine();
            Console.WriteLine("[0] Dirt");
            Console.WriteLine("[1] Rocks");
            Console.WriteLine("[2] Dirt with some Rocks in it");
            Console.WriteLine("[3] Rocks with some dirt in it");
            Console.WriteLine("[4] Back");

            while(viewing){
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    Purchase(BL, 1);
                    break;
                case "1":
                    Purchase(BL, 2);
                    break;
                case "2":
                    Purchase(BL, 3);
                    break;
                case "3":
                    Purchase(BL, 4);
                    break;
                case "4":
                    viewing = false;
                    Start(BL);
                    break;
                default:
                    Console.WriteLine("Invalid Input"); 
                    break;
            }}
        }

        public void Purchase(BLogic BL, int productId)
        {
            int orderAmount;
            int available = BL.CheckItemAmount1(productId);
            double price = BL.GetProductPrice(productId);

            while(true){
                Console.Clear();
                Console.WriteLine(available+" available");
                Console.WriteLine("How Much You Wan?");
                try{
                    orderAmount = Int32.Parse(Console.ReadLine());
                    if(orderAmount > available) throw new Exception("You Order Mo Than We Got");
                    break;
                } catch (Exception e){
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Invalid Input");
                }

            }

            Console.WriteLine("That'll be "+(price*orderAmount));
            Console.WriteLine("Would you like to Purchase?");
            Console.WriteLine("[0] Yes");
            Console.WriteLine("[1] No");

            while(true){
                switch (Console.ReadLine())
                {
                    case "0":
                        try{
                            BL.MakePurchase(productId, orderAmount);
                            Console.WriteLine("Your Order is Complete");
                            OrderOnline(BL);
                        } catch(Exception e){
                            if(e.Message != null) Console.WriteLine(e.Message);
                            Console.WriteLine("Order Failed");
                        }
                        break;
                    case "1":
                        Console.WriteLine("Order Canceled");
                        OrderOnline(BL);
                        break;
                    default: 
                        Console.WriteLine("Invalid Input");
                        break;

                }

            }

        }

        public void Nearestlocation(BLogic BL)
        {
            // will come back later if time
            BL.GetAllStores();
            Console.WriteLine("[0] Go Back");
            string input = "";
            while(input != "0"){
            input = Console.ReadLine();
            }
            Start(BL);
        }

        public void CheckOrders(BLogic BL){
            Console.Clear();
            BL.ViewTransactionsByCustomer();

            Console.WriteLine("");
            Console.WriteLine("[0] Go Back");
            while(true){
                string input = Console.ReadLine();
                if(input == "0"){
                    Start(BL);
                }else Console.WriteLine("Invalid Input");
            }
        }
    }
}