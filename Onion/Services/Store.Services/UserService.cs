using System.Collections.Generic;
using System.Linq;
using Store.Services.Abstractions;
using Store.Domain.Core.Repositories;
using Store.Domain.Core.Exceptions;
using Store.Domain.Core.Entities;
using Store.Domain.Core.Enums;
using Store.Domain.Contracts.Extensions;
using Store.Domain.Contracts.DTO;

namespace Store.Services
{
    /// <summary>
    /// Class UserService.
    /// Implements the <see cref="IUserService" />
    /// </summary>
    /// <seealso cref="IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// The order service
        /// </summary>
        private readonly IOrderService _orderService;
        /// <summary>
        /// The product service
        /// </summary>
        private readonly IProductService _productService;


        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="uow">The unit of work.</param>
        /// <param name="orderService">The order service.</param>
        /// <param name="productService">The product service.</param>
        public UserService(IUnitOfWork uow, IOrderService orderService, IProductService productService)
        {
            _unitOfWork = uow;
            _orderService = orderService;
            _productService = productService;
        }

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>RegisteredUserDto.</returns>
        public RegisteredUserDto GetUser(string id)
        {
            return GetUserById(id).ToDto();
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>IEnumerable&lt;RegisteredUserDto&gt;.</returns>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">There are no users yet.</exception>
        public IEnumerable<RegisteredUserDto> GetAllUsers()
        {
            var result = _unitOfWork.Users.GetAll();
            if (result == null)
            {
                throw new NotFoundException("There are no users yet.");
            }
            return result.Select(p => p.ToDto());
        }

        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>RegisteredUserDto.</returns>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">User with specified email was not found.</exception>
        public RegisteredUserDto FindUser(string email)
        {
            var result = _unitOfWork.Users.Find(u => u.Credentials.Email == email)
                .FirstOrDefault();

            if (result == null)
            {
                throw new NotFoundException("User with specified email was not found.");
            }
            return result.ToDto();
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <exception cref="Store.Domain.Core.Exceptions.EmailAlreadyTakenException">User with such email already exists.</exception>
        public bool CreateUser(UserModel registrationModel)
        {
            if (UserExists(registrationModel.Email))
            {
                throw new EmailAlreadyTakenException("User with such email already exists.");
            }

            var user = new RegisteredUser(
                registrationModel.FirstName,
                registrationModel.LastName,
                registrationModel.Email,
                registrationModel.Password,
                UserType.RegisteredUser);
            return _unitOfWork.Users.Create(user);
        }

        /// <summary>
        /// Changes the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newModel">The new model.</param>
        /// <exception cref="Store.Domain.Core.Exceptions.EmailAlreadyTakenException">User with such email already exists.</exception>
        public bool ChangeUser(string userId, UserModel newModel)
        {
            if (UserExists(newModel.Email))
            {
                throw new EmailAlreadyTakenException("User with such email already exists.");
            }
            var user = GetUserById(userId);            

            user.FirstName = newModel.FirstName;
            user.LastName = newModel.LastName;
            user.Credentials.Email = newModel.Email;
            user.Credentials.Password = newModel.Password;

            return _unitOfWork.Users.Update(user);
        }

        /// <summary>
        /// Adds the new order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public bool AddNewOrder(string userId)
        {
            var user = GetUserById(userId);

            var orderId = _orderService.CreateOrder();
            var order = _unitOfWork.Orders.GetById(orderId);
            user.Orders.Add(order);

            return _unitOfWork.Users.Update(user);
        }

        /// <summary>
        /// Adds to order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="productName">Name of the product.</param>
        /// <param name="amount">The amount.</param>
        public bool AddToOrder(string userId, string productName, int amount)
        {
            if (!NewOrderExists(userId))
            {
                return false;
            }
            var productId = _productService.FindProduct(productName).Id;
            var product = _unitOfWork.Products.GetById(productId);

            var user = GetUserById(userId);
           
            user.Orders[^1].Products.Add(new OrderDetails(amount, product));

            return _unitOfWork.Users.Update(user);
        }

        /// <summary>
        /// Cancels the order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="Store.Domain.Core.Exceptions.WrongStatusException">Cannot cancel this order.</exception>
        public bool CancelOrder(string userId)
        {
            if (!OrderExists(userId))
            {
                return false;
            }
            var user = GetUserById(userId);

            var status = user.Orders[^1].Status;
            if(status == StatusType.Received ||
                status == StatusType.Completed ||
                status == StatusType.CanceledByUser ||
                status == StatusType.CanceledByAdministrator)
            {
                throw new WrongStatusException("Cannot cancel this order.");
            }
            return _orderService.ChangeStatus(user.Orders[^1].Id, "CanceledByUser");
        }

        /// <summary>
        /// Receives the order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="Store.Domain.Core.Exceptions.WrongStatusException">Cannot cancel this order.</exception>
        public bool ReceiveOrder(string userId)
        {
            if (!OrderExists(userId))
            {
                return false;
            }
            var user = GetUserById(userId);

            var status = user.Orders[^1].Status;
            if (status == StatusType.Completed ||
                status == StatusType.CanceledByUser ||
                status == StatusType.CanceledByAdministrator)
            {
                throw new WrongStatusException("Cannot cancel this order.");
            }
            return _orderService.ChangeStatus(user.Orders[^1].Id, "Received");
        }

        /// <summary>
        /// Gets the user orders.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">There are no orders yet.</exception>
        public IEnumerable<OrderDto> GetUserOrders(string userId)
        {
            var user = GetUserById(userId);
            var orders = user.Orders.Select(o => o.ToDto(userId));
            if(!orders.Any())
            {
                throw new NotFoundException("There are no orders yet.");
            }
            return orders;
        }

        /// <summary>
        /// Logs the in.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>RegisteredUserDto.</returns>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">User with specified email was not found.</exception>
        /// <exception cref="Store.Domain.Core.Exceptions.WrongPasswordException">Wrong password.</exception>
        public RegisteredUserDto LogIn(string email, string password)
        {
            if(!UserExists(email))
            {
                throw new NotFoundException("User with specified email was not found.");
            }
                        
            var user = _unitOfWork.Users.Find(u => u.Credentials.Email == email)
                .FirstOrDefault();
            if(user.Credentials.Email == email &&
                user.Credentials.Password == password)
            {
                return user.ToDto();
            }
            else
            {
                throw new WrongPasswordException("Wrong password.");
            }
        }

        private bool UserExists(string email)
        {
            var users = _unitOfWork.Users.Find(u => u.Credentials.Email == email);

            return users.Any(p => p.Credentials.Email == email);
        }
       
        private bool NewOrderExists(string userId)
        {
            var user = GetUserById(userId);
            if(!OrderExists(userId))
            {
                return false;
            }
            if (user.Orders[^1].Status != StatusType.New)
            {
                throw new WrongStatusException("Cannot change order with this status.");
            }
            return true;
        }

        private bool OrderExists(string userId)
        {
            var user = GetUserById(userId);
            if (user.Orders.Count == 0)
            {
                throw new NoOrdersException("No orders were found. You need to create an order first.");
            }
            return true;
        }
       
        private RegisteredUser GetUserById(string id)
        {
            var result = _unitOfWork.Users.GetById(id);

            if (result == null)
            {
                throw new NotFoundException("Specified user was not found.");
            }
            return result;
        }        
    }
}
