using System;

namespace Store.ConsoleApp
{
    /// <summary>
    /// Class Printer for printing messages.
    /// </summary>
    public static class Printer
    {
        /// <summary>
        /// Print Start page.
        /// </summary>
        public static void StartPage()
        {
            string text = "WELCOME TO OUR CONSOLE SHOP!\n\n";
            text += "Menu guide:\n";
            Console.WriteLine(text);
        }

        /// <summary>
        /// Print guests menu.
        /// </summary>
        public static void GuestMenu()
        {
            string menu = "\n1 --> Log in\n";
            menu += "2 --> Sign up\n";
            menu += "3 --> Find product\n";
            menu += "Q --> Quit\n";
            Console.WriteLine(menu);
        }

        /// <summary>
        /// print registered user menu.
        /// </summary>
        public static void RegisteredMenu()
        {
            string menu = "\n1 --> View all products\n";
            menu += "2 --> Find product\n";
            menu += "3 --> Create new order\n";
            menu += "4 --> Order product\n";
            menu += "5 --> History of orders\n";
            menu += "6 --> Confirm receiving\n";
            menu += "7 --> Cancel order\n";
            menu += "8 --> Change personal information\n";
            menu += "9 --> Sign out\n";
            menu += "Q --> Quit\n";
            Console.WriteLine(menu);
        }

        /// <summary>
        /// print admin menu.
        /// </summary>
        public static void AdminMenu()
        {
            string menu = "\n1 --> View all products\n";
            menu += "2 --> Find product\n";
            menu += "3 --> Create new order\n";
            menu += "4 --> Order product\n";
            menu += "5 --> View all users\n";
            menu += "6 --> Change user's personal information\n";
            menu += "7 --> Add new product\n";
            menu += "8 --> Change information about the product\n";
            menu += "9 --> Order status changing\n";
            menu += "0 --> Sign out\n";
            menu += "Q --> Quit\n";
            Console.WriteLine(menu);
        }
    }
}
