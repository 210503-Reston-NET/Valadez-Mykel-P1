using System;
using BuisnessLogic;

namespace UI.Menus
{
    public class SelectedStoreMenu
    {
        public void Start(BLogic BL)
        {
            Console.Clear();
            Console.WriteLine("[0] View Inventory");
            Console.WriteLine("[1] Add Inventory");
            Console.WriteLine("[2] View Transcations");
            Console.WriteLine("[3] Back");
            string input = Console.ReadLine();

            switch(input)
            {
                case "0":
                    ViewInventory(BL);
                    break;
                case "1": 
                    AddInventory(BL);
                    break;
                case "2": 
                    ViewTransactions(BL);
                    break;
                case "3": 
                    new StoreMenu().Start(BL);
                    break;
                default:
                    Console.WriteLine("invalid input");
                    this.Start(BL);
                    break;
            }
        }

        public void ViewInventory(BLogic BL){
            try{
                BL.ViewInventory();
            }catch(Exception e){
                if(e.Message != null)Console.WriteLine(e.Message);
                Console.WriteLine("Unable to Retrieve Data");
            }
            Console.WriteLine("");
            Console.WriteLine("All Available records Retrived");
            Console.WriteLine("Press any Key to Continue");
            string hold = Console.ReadLine();
                
            Start(BL);
        }

        public void ViewTransactions(BLogic BL){
            try{
                Console.Clear();
                BL.ViewTransactionsByLocation();
            }catch(Exception e){
                if(e.Message != null) Console.WriteLine(e.Message);
                Console.WriteLine("Unable to View Transactions");
            }
            Console.WriteLine("");
            Console.WriteLine("All Records Available Have Been Displayed, Press any Key to Continue");
            string con = Console.ReadLine();
            Start(BL);
        }

        public void AddInventory(BLogic BL){
            int productId = 0;
            
            Console.Clear();
            Console.WriteLine("What Do You Want to Stock Up On?");
            Console.WriteLine();
            Console.WriteLine("[0] Dirt");
            Console.WriteLine("[1] Rocks");
            Console.WriteLine("[2] Dirt with some Rocks in it");
            Console.WriteLine("[3] Rocks with some dirt in it");
            Console.WriteLine("[4] Back");

            bool repeat = true;
            while(repeat){
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    productId = 1;
                    repeat = false;
                    break;
                case "1":
                    productId = 2;
                    repeat = false;
                    break;
                case "2":
                    productId = 3;
                    repeat = false;
                    break;
                case "3":
                    productId = 4;
                    repeat = false;
                    break;
                case "4":
                    repeat = false;
                    Start(BL); 
                    break;
                default:
                    Console.WriteLine("Invalid Input"); 
                    break;
            }}
            Console.Clear();
            Console.WriteLine("How Much Do You Want to Order?");
            Console.WriteLine("");
            
            if(Int32.TryParse(Console.ReadLine(), out int quantity)){
                try{
                    BL.AddInventory(productId, quantity);

                }catch(Exception e){
                    if(e.Message != null) Console.WriteLine(e.Message);
                    Console.WriteLine("Unable to Add Product");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Press any Key to Continue");
            string key = Console.ReadLine();

            AddInventory(BL);
        }

    }
}