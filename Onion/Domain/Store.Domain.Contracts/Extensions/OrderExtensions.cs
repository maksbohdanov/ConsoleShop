using Store.Domain.Core.Entities;
using Store.Domain.Contracts.DTO;
using System.Linq;

namespace Store.Domain.Contracts.Extensions
{
    /// <summary>
    /// Class OrderExtensions.
    /// </summary>
    public static class OrderExtensions
    {
        /// <summary>
        /// Converts to dto.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>OrderDto.</returns>
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
