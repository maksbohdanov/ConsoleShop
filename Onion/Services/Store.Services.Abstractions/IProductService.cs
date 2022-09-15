using Store.Domain.Contracts.DTO;
using System.Collections.Generic;

namespace Store.Services.Abstractions
{
    /// <summary>
    /// Interface for Product Service.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets the product by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ProductDto.</returns>
        ProductDto GetProduct(string id);
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>IEnumerable&lt;ProductDto&gt;.</returns>
        IEnumerable<ProductDto> GetAllProducts();
        /// <summary>
        /// Finds the product by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>ProductDto.</returns>
        ProductDto FindProduct(string name);
        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="productDto">The product dto.</param>
        bool CreateProduct(ProductDto productDto);
        /// <summary>
        /// Changes the product.
        /// </summary>
        /// <param name="productDto">The product dto.</param>
        bool ChangeProduct(ProductDto productDto);
    }
}
