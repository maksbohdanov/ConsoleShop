using System;

namespace Store.Infrastructure.Presentation
{
    /// <summary>
    /// Class InputHelper.
    /// </summary>
    public static class InputHelper
    {
        /// <summary>
        /// Enterings the personal data.
        /// </summary>
        /// <returns>System.ValueTuple&lt;System.String, System.String, System.String, System.String&gt;.</returns>
        public static (string, string, string, string) EnteringPersonalData()
        {
            Console.WriteLine("\nEnter your first name:\n");
            var fname = Console.ReadLine();
            Console.WriteLine("Enter your last name:\n");
            var lname = Console.ReadLine();
            Console.WriteLine("Enter your email:\n");
            var email = Console.ReadLine();
            Console.WriteLine("Enter your password:\n");
            var password = Console.ReadLine();
            return (fname, lname, email, password);
        }
        /// <summary>
        /// Enterings the credentials.
        /// </summary>
        /// <returns>System.ValueTuple&lt;System.String, System.String&gt;.</returns>
        public static (string, string) EnteringCredentials()
        {
            Console.WriteLine("\nEnter your email:\n");
            var email = Console.ReadLine();
            Console.WriteLine("Enter your password:\n");
            var password = Console.ReadLine();
            return (email, password);
        }

        /// <summary>
        /// Enterings the data to change.
        /// </summary>
        /// <returns>System.ValueTuple&lt;System.String, System.String, System.String, System.String&gt;.</returns>
        public static (string, string, string, string) EnteringDataToChange()
        {
            Console.WriteLine("\nEnter new first name:\n");
            var fname = Console.ReadLine();
            Console.WriteLine("Enter new last name:\n");
            var lname = Console.ReadLine();
            Console.WriteLine("Enter new email:\n");
            var email = Console.ReadLine();
            Console.WriteLine("Enter new password:\n");
            var password = Console.ReadLine();
            return (fname, lname, email, password);
        }
        /// <summary>
        /// Enterings the product data.
        /// </summary>
        /// <returns>System.ValueTuple&lt;System.String, System.String, System.String, System.Int32&gt;.</returns>
        public static (string, string, string, int) EnteringProductData()
        {
            int cost;
            string costInput;
            Console.WriteLine("\nEnter the name of the product:\n");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the category:\n");
            var category = Console.ReadLine();
            Console.WriteLine("Enter short description:\n");
            var desc = Console.ReadLine();
            do
            {
                Console.WriteLine("Enter cost of the product:");
                costInput = Console.ReadLine();
            } while (!int.TryParse(costInput, out cost));

            return (name, category, desc, cost);
        }
        /// <summary>
        /// Enterings the product and amount.
        /// </summary>
        /// <returns>System.ValueTuple&lt;System.String, System.Int32&gt;.</returns>
        public static (string, int) EnteringProductAndAmount()
        {
            Console.WriteLine("\nEnter the name of the product:\n");
            var name = Console.ReadLine();
            int amount;
            string amountInput;
            do
            {
                Console.WriteLine("Enter amount of the product:");
                amountInput = Console.ReadLine();
            } while (!int.TryParse(amountInput, out amount));

            return (name, amount);
        }
        /// <summary>
        /// Enterings the status and identifier.
        /// </summary>
        /// <returns>System.ValueTuple&lt;System.String, System.String&gt;.</returns>
        public static (string, string) EnteringStatusAndId()
        {
            Console.WriteLine("Enter the order id:\n");
            var orderId = Console.ReadLine();
            Console.WriteLine("\nEnter status type:\n1 --> Canceled by the administrator\n2 --> Payment received\n3 --> Sent\n4 --> Completed\n");
            var command = Console.ReadKey(true);
            string status = string.Empty;
            switch (command.Key)
            {
                case ConsoleKey.D1:
                    {
                        status = "CanceledByAdministrator";
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        status = "PaymentReceived";
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        status = "Sent";
                        break;
                    }
                case ConsoleKey.D4:
                    {
                        status = "Completed";
                        break;
                    }
            }
            return (orderId, status);
        }
    }
}
