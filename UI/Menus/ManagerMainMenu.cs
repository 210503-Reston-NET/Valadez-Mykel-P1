using Models;
using System;
using BuisnessLogic;
using DataLogic; 

namespace UI.Menus
{
    public class ManagerMainMenu
    {
        public void Start(BLogic BL)
        {
            
            Console.Clear();
            Console.WriteLine("What Would You Like to Do?");
            Console.WriteLine("[0] Find a Customer");
            Console.WriteLine("[1] Find an Order");
            Console.WriteLine("[2] Manage a Store Location");
            Console.WriteLine("[3] View All Locations");
            Console.WriteLine("[4] Log Out");
            
            string input = Console.ReadLine();

            switch(input)
            {
                case "0":
                    CustomerFinder(BL);
                    break;
                case "1":
                    OrderFinder(BL);
                    break;
                case "2":
                    new StoreMenu().Start(BL);
                    break;
                case "3":
                    GetAllStores(BL);
                    break;
                case "4":
                    new LoginMenu().Start();
                    break;
                default:
                    this.Start(BL);
                    break;
            }
        }

        public void CustomerFinder(BLogic BL)
        {
            Console.Clear();
            Console.WriteLine("What Would you like to Search by?");
            Console.WriteLine("[0] Customer Id");
            Console.WriteLine("[1] Customer Name");
            Console.WriteLine("[2] Back");

            while(true){
                switch(Console.ReadLine()){
                    case "1":
                        Console.WriteLine("Enter in the Customer Name: ");
                        try{
                            BL.FindUser(Console.ReadLine());
                        }catch{
                            Console.WriteLine("unable to Find Customer");
                        }
                        Console.WriteLine("");
                        Console.WriteLine("Press any Key to Continue");
                        string hold = Console.ReadLine();
                        Start(BL);
                        break;
                    case "0":
                        Console.WriteLine("Enter in the Customer Id: ");
                        string input = Console.ReadLine();
                        if(Int32.TryParse(input, out int id)){
                            try{
                                BL.FindUser(id);
                            }catch(Exception e){
                                if(e.Message != null)Console.WriteLine(e.Message);
                                Console.WriteLine("Unable to Retrieve Data");
                            }

                        }else {
                            Console.WriteLine("Invalid Input");
                        }
                        Console.WriteLine("");
                        Console.WriteLine("Press any Key to Continue");
                        string hol = Console.ReadLine();
                        Start(BL);
                        break;
                    case "2":
                        Start(BL);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;

                }
            }
        }

        public void OrderFinder(BLogic BL)
        {
            Console.Clear();
            Console.WriteLine("Enter the Order Id");

            if(Int32.TryParse(Console.ReadLine(), out int id)){
                try{
                    BL.CheckOrder(id);
                }catch(Exception e){
                if(e.Message != null)Console.WriteLine(e.Message);
                Console.WriteLine("Unable to Retrieve Data");
                }
            }else{
                Console.WriteLine("Input Must be a Number");
            }
            Console.WriteLine("");
            Console.WriteLine("Press any Key to Continue");
            string hold = Console.ReadLine();
            Start(BL);
        }

        public void GetAllStores(BLogic BL){
            Console.Clear();
            try{
                BL.GetAllStores();
            }catch(Exception e){
                if(e.Message != null)Console.WriteLine(e.Message);
                Console.WriteLine("Unable to Retrieve Data");
            }
            Console.WriteLine("");
            Console.WriteLine("Press any Key to Continue");
            string hold = Console.ReadLine();
            Start(BL);
            

        }

    }
}