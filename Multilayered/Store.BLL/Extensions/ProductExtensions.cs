using Store.BLL.DTO;
using Store.DAL.Entities;

namespace Store.BLL.Extensions
{
    public static class ProductExtensions
    {
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
