using Store.BLL.DTO;
using System.Collections.Generic;

namespace Store.BLL.Interfaces
{
    public interface IOrderService
    {
        OrderDto GetOrder(string id, string userId);
        IEnumerable<OrderDto> GetAllOrders(string userId);
        string CreateOrder();        
        bool ChangeStatus(string orderId, string newStatus);
    }
}
