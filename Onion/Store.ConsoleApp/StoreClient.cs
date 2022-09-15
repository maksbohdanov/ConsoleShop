using System;
using System.Collections.Generic;
using Store.Infrastructure.Presentation;

namespace Store.ConsoleApp
{
    /// <summary>
    /// Class StoreClient.
    /// </summary>
    public class StoreClient
    {
        /// <summary>
        /// Delegate Command
        /// </summary>
        /// <returns>System.String.</returns>
        private delegate string Command();
        /// <summary>
        /// The commands
        /// </summary>
        private Dictionary<string, Command> commands;
        /// <summary>
        /// The controller
        /// </summary>
        private readonly Controller _controller;
        /// <summary>
        /// The quit option
        /// </summary>
        private bool quit = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreClient"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public StoreClient(Controller controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Chooses the command.
        /// </summary>
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

        /// <summary>
        /// Represent guests presentation.
        /// </summary>
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

        /// <summary>
        /// Represent registered user presentation.
        /// </summary>
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

        /// <summary>
        /// Represent admins presentation.
        /// </summary>
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

        /// <summary>
        /// Handle presentations depends on user type.
        /// </summary>
        private void UserHandler()
        {
            switch (_controller.UserType)
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
