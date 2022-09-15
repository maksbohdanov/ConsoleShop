namespace Store.Domain.Core.Entities
{
    /// <summary>
    /// Class OrderDetails used for creating orders.
    /// </summary>
    public class OrderDetails
    {
        /// <summary>
        /// Gets or sets the amount of the product.
        /// </summary>
        /// <value>The amount.</value>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>The product.</value>
        public Product Product { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetails"/> class.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="product">The product.</param>
        public OrderDetails(int amount, Product product)
        {
            Amount = amount;
            Product = product;
        }
    }
}
