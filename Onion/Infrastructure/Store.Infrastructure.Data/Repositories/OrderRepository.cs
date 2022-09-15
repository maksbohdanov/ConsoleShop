using System;
using System.Collections.Generic;
using System.Linq;
using Store.Domain.Core.Entities;
using Store.Domain.Core.Repositories;

namespace Store.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository for working with orders.
    /// Implements the <see cref="Store.Domain.Core.Repositories.IRepository{Store.Domain.Core.Entities.Order}" />
    /// </summary>
    /// <seealso cref="Store.Domain.Core.Repositories.IRepository{Store.Domain.Core.Entities.Order}" />
    public class OrderRepository : IRepository<Order>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;BaseEntity&gt;.</returns>
        public IEnumerable<Order> GetAll()
        {
            return StoreContext.Orders;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>BaseEntity.</returns>
        public Order GetById(string id)
        {
            return StoreContext.Orders.Find(p => p.Id == id);
        }

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;Order&gt;.</returns>
        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return StoreContext.Orders.Where(predicate).ToList();
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public bool Create(Order entity)
        {
            StoreContext.Orders.Add(entity);

            return StoreContext.Orders.FirstOrDefault(p => p.Id == entity.Id) != null;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public bool Update(Order entity)
        {
            var toUpdate = StoreContext.Orders.FirstOrDefault(p => p.Id == entity.Id);
            if (toUpdate is null)
                return false;

            toUpdate = entity;

            return StoreContext.Orders.Contains(toUpdate);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public bool Delete(string id)
        {
            StoreContext.Orders.RemoveAll(p => p.Id == id);

            return StoreContext.Orders.FirstOrDefault(p => p.Id == id) == null;
        }
    }
}
