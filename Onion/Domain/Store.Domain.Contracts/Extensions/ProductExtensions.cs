using Store.Domain.Core.Entities;
using Store.Domain.Contracts.DTO;

namespace Store.Domain.Contracts.Extensions
{
    /// <summary>
    /// Class ProductExtensions.
    /// </summary>
    public static class ProductExtensions
    {
        /// <summary>
        /// Converts to dto.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>ProductDto.</returns>
        public static ProductDto ToDto(this Product product, int amount = 0)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Description = product.Description,
                Cost = product.Cost,
                Amount = amount
            };
        }
    }
}
