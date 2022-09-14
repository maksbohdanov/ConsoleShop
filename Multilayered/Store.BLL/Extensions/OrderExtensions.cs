using Store.BLL.DTO;
using Store.DAL.Entities;
using System.Linq;

namespace Store.BLL.Extensions
{
    public static class OrderExtensions
    {
        public static OrderDto ToDto(this Order order, string userId)
        {
            return new OrderDto()
            {
                Id = order.Id,
                Status = order.Status.ToString(),
                UserId = userId,
                Products = order.Products
                    .Select(p => p.Product.ToDto(p.Amount)).ToList()
            };
        }
    }
}
