using System;
using System.Collections.Generic;

namespace Store.ConsoleApp
{     
    public class StoreClient
    {
        public static string UserId { get; set; }
        public static string UserType { get; set; } = "Guest";

        private delegate string Command();        
        private Dictionary<string, Command> commands;
        private readonly Controller _controller;
        private bool quit = false;

        public StoreClient(Controller controller)
        {
            _controller = controller;
        }

        private void ChooseCommand()
        {
            var commandKey = Console.ReadKey(true);
            
            if (commandKey.Key == ConsoleKey.Q)
            {
                quit = true;
                return;
            }
            if (commands.TryGetValue(commandKey.KeyChar.ToString(), out Command command))
            {
                Console.WriteLine(command()); 
            }
            else
                Console.WriteLine("\nUnknown command.\n");
        }

        private void GuestPresentation()
        {            
            Printer.GuestMenu();
            commands = new Dictionary<string, Command>()
            {
                { "1", _controller.LogIn },
                { "2", _controller.SignUp },
                { "3", _controller.FindProductByName },
            };
            ChooseCommand();
        }
        
        private void RegisteredPresentation()
        {           
            Printer.RegisteredMenu();
            commands = new Dictionary<string, Command>()
            {
                { "1", _controller.GetAllProducts },
                { "2", _controller.FindProductByName },
                { "3", _controller.CreateOrder },
                { "4", _controller.AddToOrder },
                { "5", _controller.ViewAllOrders },
                { "6", _controller.ReceiveOrder },
                { "7", _controller.CancelOrder },
                { "8", _controller.ChangePersonalInfo },
                { "9", _controller.SignOut },
            };
            ChooseCommand();
        }

        private void AdminPresentation()
        {
            Printer.AdminMenu();
            commands = new Dictionary<string, Command>()
            {
                { "1", _controller.GetAllProducts },
                { "2", _controller.FindProductByName },
                { "3", _controller.CreateOrder },
                { "4", _controller.AddToOrder },
                { "5", _controller.ViewAllUsers },
                { "6", _controller.ChangePersonalInfoByAdmin },
                { "7", _controller.AddNewProduct },
                { "8", _controller.ChangeProduct },
                { "9", _controller.ChangeStatus },
                { "0", _controller.SignOut },
            };
            ChooseCommand();
        }

        private void UserHandler()
        {
            switch (UserType)
            {
                case "Guest":
                    {
                        GuestPresentation();
                        break;
                    }
                case "RegisteredUser":
                    {
                        RegisteredPresentation();
                        break;
                    }
                case "Administrator":
                    {
                        AdminPresentation();
                        break;
                    }
            }
        }
        public void Start()
        {
            Printer.StartPage();
            while (!quit)
            {
                UserHandler();
            }                         
        }        
    }
}
