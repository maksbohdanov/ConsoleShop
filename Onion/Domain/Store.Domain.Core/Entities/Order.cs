using Store.Domain.Core.Enums;
using System.Collections.Generic;

namespace Store.Domain.Core.Entities
{
    /// <summary>
    /// Class Order used for creating orders.
    /// </summary>
    public class Order: BaseEntity
    {
        /// <summary>
        /// Gets or sets the products in an order.
        /// </summary>
        /// <value>The products.</value>
        public List<OrderDetails> Products { get; set; }

        /// <summary>
        /// Gets or sets order status.
        /// </summary>
        /// <value>The status.</value>
        public StatusType Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order" /> class.
        /// </summary>
        public Order()
        {            
            Status = StatusType.New;
            Products = new List<OrderDetails>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order" /> class.
        /// </summary>
        /// <param name="products">The products.</param>
        public Order(List<OrderDetails> products) : this()
        {
            Products = products;
        }
    }
}
