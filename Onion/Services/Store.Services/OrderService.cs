using System;
using System.Collections.Generic;
using Store.Services.Abstractions;
using Store.Domain.Core.Repositories;
using Store.Domain.Core.Exceptions;
using Store.Domain.Core.Entities;
using Store.Domain.Core.Enums;
using Store.Domain.Contracts.Extensions;
using Store.Domain.Contracts.DTO;
using System.Linq;

namespace Store.Services
{
    /// <summary>
    /// Class OrderService.
    /// Implements the <see cref="IOrderService" />
    /// </summary>
    /// <seealso cref="IOrderService" />
    public class OrderService : IOrderService
    {
        /// <summary>
        /// The unit of work.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;


        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public OrderService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        /// Gets the order by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>OrderDto.</returns>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">Specified order was not found.</exception>
        public OrderDto GetOrder(string id, string userId)
        {
            var result = _unitOfWork.Orders.GetById(id);
            if (result == null)
            {
                throw new NotFoundException("Specified order was not found.");
            }
            return result.ToDto(userId);
        }


        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IEnumerable&lt;OrderDto&gt;.</returns>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">There are no orders yet.</exception>
        public IEnumerable<OrderDto> GetAllOrders(string userId)
        {
            var result = _unitOfWork.Orders.GetAll();
            if (result == null)
            {
                throw new NotFoundException("There are no orders yet.");
            }
            return result.Select(p => p.ToDto(userId));
        }

        /// <summary>
        /// Creates the order.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="Store.Domain.Core.Exceptions.OrderCreationException">Unable to create an order.</exception>
        public string CreateOrder()
        {
            var order = new Order();            
            if(!_unitOfWork.Orders.Create(order))
            {
                throw new OrderCreationException("Unable to create an order.");
            }
            
            return order.Id;
        }

        /// <summary>
        /// Changes the order status.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="newStatus">The new status.</param>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">Specified order was not found.</exception>
        public bool ChangeStatus(string orderId, string newStatus)
        {
            var order = _unitOfWork.Orders.GetById(orderId);
            if (order == null)
            {
                throw new NotFoundException("Specified order was not found.");
            }

            Enum.TryParse(newStatus, out StatusType status);
            order.Status = status;           

            return _unitOfWork.Orders.Update(order);
        }
    }
}
