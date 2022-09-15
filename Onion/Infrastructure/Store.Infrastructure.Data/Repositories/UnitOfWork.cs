using Store.Domain.Core.Entities;
using Store.Domain.Core.Repositories;

namespace Store.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Class UnitOfWork.
    /// Implements the <see cref="IUnitOfWork" />
    /// </summary>
    /// <seealso cref="IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The product repository
        /// </summary>
        private ProductRepository productRepository;
        /// <summary>
        /// The order repository
        /// </summary>
        private OrderRepository orderRepository;
        /// <summary>
        /// The user repository
        /// </summary>
        private UserRepository userRepository;

        /// <summary>
        /// Gets the product repository.
        /// </summary>
        /// <value>The products.</value>
        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository();
                return productRepository;
            }
        }

        /// <summary>
        /// Gets the orders repository.
        /// </summary>
        /// <value>The orders.</value>
        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository();
                return orderRepository;
            }
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>The users.</value>
        public IRepository<RegisteredUser> Users
        {
            get
            {
                if(userRepository == null)
                    userRepository = new UserRepository();
                return userRepository;
            }
        }
    }
}
