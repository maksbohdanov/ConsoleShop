using System;
using System.Linq;
using Ninject;
using Store.Services.Abstractions;
using Store.Services;
using Store.Domain.Core.Exceptions;
using Store.Domain.Contracts.DTO;
using Store.Infrastructure.Presentation.FormatExtensions;

namespace Store.Infrastructure.Presentation
{
    /// <summary>
    /// Class Controller.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>The type of the user.</value>
        public string UserType { get; set; } = "Guest";

        /// <summary>
        /// The order service
        /// </summary>
        private readonly IOrderService _orderService;
        /// <summary>
        /// The product service
        /// </summary>
        private readonly IProductService _productService;
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService _userService;


        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public Controller(IKernel kernel)
        {
            _orderService = kernel.Get<OrderService>();
            _productService = kernel.Get<ProductService>();
            _userService = kernel.Get<UserService>();
        }
        
        public string SignUp()
        {
            (string fname, string lname, string email, string password) = InputHelper.EnteringPersonalData();
            if (string.IsNullOrEmpty(fname) ||
                string.IsNullOrEmpty(lname) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password))
            {
                return "\nFields cannot be empty.";
            }

            try
            {
                var user = new UserModel()
                {
                    FirstName = fname,
                    LastName = lname,
                    Email = email,
                    Password = password
                };
                return _userService.CreateUser(user) ? "\nAccount created." : "\nCannot create an account.";
            }
            catch (EmailAlreadyTakenException ex)
            {
                return ex.Message;
            }
        }
        
        public string SignOut()
        {
            UserId = null;
            UserType = "Guest";
            return "\nYou have signed out from the account.";
        }
        
        public string LogIn()
        {
            (string email, string password) = InputHelper.EnteringCredentials();
            if (string.IsNullOrEmpty(email) ||
               string.IsNullOrEmpty(password))
            {
                return "\nFields cannot be empty.";
            }
            try
            {
                var user = _userService.LogIn(email, password);
                UserId = user.Id;
                UserType = user.UserType;
                return "\nYou are logged in.";
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }
            catch (WrongPasswordException ex)
            {
                return ex.Message;
            }

        }

        public string FindProductByName()
        {
            Console.WriteLine("\nEnter the name of the product:\n");
            var name = Console.ReadLine();

            try
            {
                var product = _productService.FindProduct(name);
                return product.Format();
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }            
        }

        public string GetAllProducts()
        {           
            try
            {
                var products = _productService.GetAllProducts();
                return string.Join("\n", products.Select(p => p.Format()));
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }
        }
        
        public string AddToOrder()
        {
            (string name, int amount) = InputHelper.EnteringProductAndAmount();                     
            try
            {
                if (amount < 1) { return "\nAmount cannot be lower than 1."; }
                var result = _userService.AddToOrder(UserId, name, amount);
                return result ? "\nAdded to the order." : "\nCannot add to the order.";
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }
            catch (NoOrdersException ex)
            {
                return ex.Message;
            }
            catch (WrongStatusException ex)
            {
                return ex.Message;
            }
        }
       
        public string CancelOrder()
        {
            try
            {
                var result = _userService.CancelOrder(UserId);
                return result ? "\nOrder canceled." : "\nCannot cancel the order.";
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }
            catch (NoOrdersException ex)
            {
                return ex.Message;
            }
            catch (WrongStatusException ex)
            {
                return ex.Message;
            }
        }

        public string ReceiveOrder()
        {
            try
            {
                var result = _userService.ReceiveOrder(UserId);
                return result ? "\nOrder received." : "\nCannot receive the order.";
            }           
            catch (NoOrdersException ex)
            {
                return ex.Message;
            }
            catch (WrongStatusException ex)
            {
                return ex.Message;
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }
        }

        public string ChangeStatus()
        {
            (string orderId, string status) = InputHelper.EnteringStatusAndId();
            try
            {
                var result = _orderService.ChangeStatus(orderId, status);
                return result ? "\nStatus changed." : "\nCannot change the status.";
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }
        }

        public string CreateOrder()
        {
            try
            {                
                var result = _userService.AddNewOrder(UserId);

                return result ? "\nOrder was created successfully." : "\nCannot create an order.";               
            }
            catch (OrderCreationException ex)
            {
                return ex.Message;
            }
            catch(NotFoundException ex)
            {
                return ex.Message;
            }
        }

        public string ViewAllOrders()
        {
            try
            {
                var orders = _userService.GetUserOrders(UserId);
                return string.Join("\n", orders.Select(o => o.Format()));
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }
        }

        public string ChangePersonalInfo()
        {
            (string fname, string lname, string email, string password) = InputHelper.EnteringDataToChange();
            if(string.IsNullOrEmpty(fname) ||
                string.IsNullOrEmpty(lname) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password))
            {
                return "\nFields cannot be empty.";
            }
            var userModel = new UserModel()
            {
                FirstName = fname,
                LastName = lname,
                Email = email,
                Password = password
            };
            try
            {
                var result = _userService.ChangeUser(UserId, userModel);
                return result ? "\nData was changed" : "\nCannot change personal data.";
            }
            catch (EmailAlreadyTakenException ex)
            {
                return ex.Message;
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }            
        }

        public string ChangePersonalInfoByAdmin()
        {
            Console.WriteLine("Enter user's email:\n");
            var email = Console.ReadLine();
            try
            {
                var user = _userService.FindUser(email);
                (string fname, string lname, string newEmail, string password) = InputHelper.EnteringDataToChange();
                if (string.IsNullOrEmpty(fname) ||
                    string.IsNullOrEmpty(lname) ||
                    string.IsNullOrEmpty(newEmail) ||
                    string.IsNullOrEmpty(password))
                {
                    return "\nFields cannot be empty.";
                }
                var userModel = new UserModel()
                {
                    FirstName = fname,
                    LastName = lname,
                    Email = newEmail,
                    Password = password
                };
                var result = _userService.ChangeUser(user.Id, userModel);
                return result ? "\nData was changed" : "\nCannot change personal data.";
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }
        }

        public string AddNewProduct()
        {
            (string name, string category, string description, int cost) = InputHelper.EnteringProductData();
            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(category) ||
                string.IsNullOrEmpty(description))
            {
                return "\nFields cannot be empty.";
            }
            if(cost < 1) { return "\nCost cannot be lower than 1."; }

            var product = new ProductDto()
            {
                Name = name,
                Category = category,
                Description = description,
                Cost = cost
            };
            try
            {
                var result = _productService.CreateProduct(product);
                return result ? "\nNew product added." : "\nCannot add new product.";
            }
            catch (ProductAlreadyExistsException ex)
            {
                return ex.Message;
            }
        }

        public string ChangeProduct()
        {
            Console.WriteLine("Enter old name of the product you want to change:");
            string oldName = Console.ReadLine();
            try
            {
                var product = _productService.FindProduct(oldName);
                (string newName, string category, string description, int cost) = InputHelper.EnteringProductData();
                if (string.IsNullOrEmpty(newName) ||
                    string.IsNullOrEmpty(category) ||
                    string.IsNullOrEmpty(description))
                {
                    return "\nFields cannot be empty.";
                }
                if (cost < 1) { return "\nCost cannot be lower than 1."; }
                product.Name = newName;
                product.Category = category;
                product.Description = description;
                product.Cost = cost;

                var result = _productService.ChangeProduct(product);
                return result ? "\nProduct was changed." : "\nCannot change producct info.";

            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }           
        }

        public string ViewAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return string.Join("\n", users.Select(u => u.Format()));
            }
            catch (NotFoundException ex)
            {
                return ex.Message;
            }
        }
    }
}
