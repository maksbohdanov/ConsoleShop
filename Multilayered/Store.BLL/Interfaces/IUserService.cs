using Store.BLL.DTO;
using System.Collections.Generic;

namespace Store.BLL.Interfaces
{
    public interface IUserService
    {
        RegisteredUserDto GetUser(string id);
        IEnumerable<RegisteredUserDto> GetAllUsers();
        RegisteredUserDto FindUser(string email);
        bool CreateUser(UserModel registrationModel);        
        bool ChangeUser(string userId, UserModel newModel);
        bool AddNewOrder(string userId);
        IEnumerable<OrderDto> GetUserOrders(string userId);
        bool AddToOrder(string userId, string productName, int amount);
        bool CancelOrder(string userId);
        bool ReceiveOrder(string userId);
        RegisteredUserDto LogIn(string email, string password);
    }
}
