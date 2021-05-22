using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using DataLogic;
using BuisnessLogic;


namespace UI.Menus
{
    public class LoginMenu
    {
        public void Start()
        {
            BLogic BL = ConnectToDBAndBL();

            AskForLogin(BL);
        }

        public void AskForLogin(BLogic BL){
            Console.Clear();
            Console.WriteLine("Sign in or Create a new account: ");
            Console.WriteLine("[0] Sign In");
            Console.WriteLine("[1] Create New Account");
            Console.WriteLine("[2] Exit");
            

            while(true){
                string input = Console.ReadLine();
                switch(input)
                {
                    case "0": 
                        UserLogin(BL);
                        break;
                    case "1": 
                        NewUserLogin(BL);
                        break;
                    case "2": 
                        Console.Clear();
                        System.Environment.Exit(0);
                        break;
                    default: 
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
        }

        public void NewUserLogin(BLogic BL)
        {            
            string email;
            string password;
            string name;

            Console.Clear();
            Console.WriteLine("Enter Your Name");
            Console.WriteLine("");

            Console.Write("Name: ");
            name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Enter Your Email");
            Console.WriteLine("");

            Console.Write("Email: ");
            email = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Create a Password");
            Console.WriteLine("");

            Console.Write("Password: ");
            password = Console.ReadLine();

            try{
                BL.AddNewUser(name, email, password);

            } catch (Exception e){
                Console.WriteLine(e.Message);
                Console.WriteLine("");
                AskForLogin(BL);
            }
            CheckAdminAndPass(email, password, BL);

        }

        public void UserLogin(BLogic BL)
        {
            bool success = false;
            string email;
            string password;
            Console.Clear();
        
            Console.WriteLine("Enter Your Email");
            Console.WriteLine("");
            Console.Write("Email: ");
            email = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Enter Your Password");
            Console.WriteLine("");
            Console.Write("Password: ");
            password = Console.ReadLine();
            Console.WriteLine("");

            try{
                BL.CheckUserCredentials(email, password);
                success = true;
            } catch {
                Console.WriteLine("Unable to Login");
                Console.WriteLine("");
            }

            if(success){
                CheckAdminAndPass(email, password, BL);
            } else{
                AskForLogin(BL);

            }
            
        }

        public BLogic ConnectToDBAndBL(){
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            
            string connectionString = configuration.GetConnectionString("StoreDB");

            DbContextOptions<StoreDBContext> options = new DbContextOptionsBuilder<StoreDBContext>()
            .UseNpgsql(connectionString)
            .Options;

            return new BLogic( new storeDB(new StoreDBContext(options)));
        }

        public void CheckAdminAndPass(string email, string password, BLogic BL)
        {


            bool admin = false;
            if(email == "dirtEmpire@gmail.com" && password == "fishTaco")
            {
                admin = true;

            }

            if(admin)
            {
                try
                {
                    new ManagerMainMenu().Start(BL);
                } 
                catch {
                    System.Console.WriteLine("oh no error creating a manager");
                }
            } else {
                try
                {
                    new CustMainMenu().Start(BL);
                } catch {
                    System.Console.WriteLine("oh no unable to create customer account");
                }
            }
        }
    }
}