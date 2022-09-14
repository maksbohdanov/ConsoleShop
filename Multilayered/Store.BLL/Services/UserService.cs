using System.Collections.Generic;
using System.Linq;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Interfaces;
using Store.BLL.Extensions;
using Store.BLL.Exceptions;
using Store.DAL.Users;
using Store.DAL.Enums;
using Store.DAL.Entities;

namespace Store.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;


        public UserService(IUnitOfWork uow, IOrderService orderService, IProductService productService)
        {
            _unitOfWork = uow;
            _orderService = orderService;
            _productService = productService;
        }

        public RegisteredUserDto GetUser(string id)
        {
            return GetUserById(id).ToDto();
        }

        public IEnumerable<RegisteredUserDto> GetAllUsers()
        {
            var result = _unitOfWork.Users.GetAll();
            if (result == null)
            {
                throw new NotFoundException("There are no users yet.");
            }
            return result.Select(p => p.ToDto());
        }

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

        public bool AddNewOrder(string userId)
        {
            var user = GetUserById(userId);

            var orderId = _orderService.CreateOrder();
            var order = _unitOfWork.Orders.GetById(orderId);
            user.Orders.Add(order);

            return _unitOfWork.Users.Update(user);
        }

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
