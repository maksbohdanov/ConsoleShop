using Store.Domain.Core.Entities;

namespace Store.Domain.Core.Repositories
{
    /// <summary>
    /// Intarface of Unit of work pattern.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the product repository.
        /// </summary>
        /// <value>The products.</value>
        IRepository<Product> Products { get; }
        /// <summary>
        /// Gets the orders repository.
        /// </summary>
        /// <value>The orders.</value>
        IRepository<Order> Orders { get; }
        /// <summary>
        /// Gets the users repository.
        /// </summary>
        /// <value>The users.</value>
        IRepository<RegisteredUser> Users { get; }
    }
}
