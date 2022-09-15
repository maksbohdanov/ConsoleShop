using Store.Domain.Contracts.DTO;
using System.Collections.Generic;

namespace Store.Services.Abstractions
{
    /// <summary>
    /// Interface for User Service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>RegisteredUserDto.</returns>
        RegisteredUserDto GetUser(string id);
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>IEnumerable&lt;RegisteredUserDto&gt;.</returns>
        IEnumerable<RegisteredUserDto> GetAllUsers();
        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>RegisteredUserDto.</returns>
        RegisteredUserDto FindUser(string email);
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        bool CreateUser(UserModel registrationModel);
        /// <summary>
        /// Changes the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newModel">The new model.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ChangeUser(string userId, UserModel newModel);
        /// <summary>
        /// Adds the new order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        bool AddNewOrder(string userId);
        /// <summary>
        /// Gets the user orders.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        IEnumerable<OrderDto> GetUserOrders(string userId);
        /// <summary>
        /// Adds to order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="productName">Name of the product.</param>
        /// <param name="amount">The amount.</param>
        bool AddToOrder(string userId, string productName, int amount);
        /// <summary>
        /// Cancels the order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        bool CancelOrder(string userId);
        /// <summary>
        /// Receives the order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        bool ReceiveOrder(string userId);
        /// <summary>
        /// Logs the in.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>RegisteredUserDto.</returns>
        RegisteredUserDto LogIn(string email, string password);
    }
}
