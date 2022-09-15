using Store.Domain.Contracts.DTO;
using System.Collections.Generic;

namespace Store.Services.Abstractions
{
    /// <summary>
    /// Interface for Order Service.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Gets the order by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>OrderDto.</returns>
        OrderDto GetOrder(string id, string userId);

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IEnumerable&lt;OrderDto&gt;.</returns>
        IEnumerable<OrderDto> GetAllOrders(string userId);
        /// <summary>
        /// Creates the order.
        /// </summary>
        /// <returns>System.String.</returns>
        string CreateOrder();
        /// <summary>
        /// Changes the order status.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="newStatus">The new status.</param>
        bool ChangeStatus(string orderId, string newStatus);
    }
}
