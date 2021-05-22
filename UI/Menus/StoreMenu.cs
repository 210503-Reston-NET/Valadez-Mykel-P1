using System;
using BuisnessLogic;
using UI;

namespace UI.Menus
{
    public class StoreMenu
    {
        public void Start(BLogic BL)
        {
            Console.Clear();
            Console.WriteLine("[0] Search for Location");
            Console.WriteLine("[1] Add a Location");
            Console.WriteLine("[2] Back");
            string input = Console.ReadLine();

            switch(input)
            {
                case "0":
                    LocationSearch(BL);
                    break;
                case "1": 
                    AddLocation(BL);
                    break;
                case "2": 
                    new ManagerMainMenu().Start(BL);
                    break;
                default:
                    this.Start(BL);
                    break;
            }
        }

        public void LocationSearch(BLogic BL)
        {
            Console.Clear();
            Console.WriteLine("Enter the Name of the Location: ");
            
            try{
                BL.FindLocationByName(Console.ReadLine());
                new SelectedStoreMenu().Start(BL);
            }catch(Exception e){
                System.Console.WriteLine(e.Message);
                Console.WriteLine("Unable To Find Store, Press Any Key to Continue");
                string inpu = Console.ReadLine();
                
                    Start(BL);
                
            }


        }

        public void AddLocation(BLogic BL)
        {
            Console.WriteLine("Enter the Name of the New Location: ");
            string locationName = Console.ReadLine();

            Console.WriteLine("");
            Console.WriteLine("Enter the Address of the New Location: ");
            string locationAddress = Console.ReadLine();

            try{
                BL.AddLocation(locationName, locationAddress);
                Console.WriteLine("New Store Added");
            }catch(Exception e){
                if(e != null) Console.WriteLine(e.Message);
                Console.WriteLine("Unable to Add New Location");

            }

            Console.WriteLine("Press any Key to Continue");

            string input = Console.ReadLine();
            Start(BL);
        }
    }
}