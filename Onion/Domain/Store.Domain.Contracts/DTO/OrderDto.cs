using System.Collections.Generic;

namespace Store.Domain.Contracts.DTO
{
    /// <summary>
    /// Class OrderDto, used for working with services.
    /// </summary>

    public class OrderDto: BaseEntityDto
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        public List<ProductDto> Products { get; set; }
    }
}
