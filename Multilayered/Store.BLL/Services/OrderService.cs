using System;
using System.Collections.Generic;
using Store.BLL.Interfaces;
using Store.BLL.DTO;
using Store.BLL.Exceptions;
using Store.BLL.Extensions;
using Store.DAL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Enums;
using System.Linq;

namespace Store.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;


        public OrderService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public OrderDto GetOrder(string id, string userId)
        {
            var result = _unitOfWork.Orders.GetById(id);
            if (result == null)
            {
                throw new NotFoundException("Specified order was not found.");
            }
            return result.ToDto(userId);
        }


        public IEnumerable<OrderDto> GetAllOrders(string userId)
        {
            var result = _unitOfWork.Orders.GetAll();
            if (result == null)
            {
                throw new NotFoundException("There are no orders yet.");
            }
            return result.Select(p => p.ToDto(userId));
        }

        public string CreateOrder()
        {
            var order = new Order();            
            if(!_unitOfWork.Orders.Create(order))
            {
                throw new OrderCreationException("Unable to create an order.");
            }
            
            return order.Id;
        }        

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
