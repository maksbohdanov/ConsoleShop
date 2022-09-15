using System;
using System.Collections.Generic;
using System.Linq;
using Store.Domain.Core.Entities;
using Store.Domain.Core.Repositories;

namespace Store.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository for working with products.
    /// Implements the <see cref="Store.Domain.Core.Repositories.IRepository{Store.Domain.Core.Entities.Product}" />
    /// </summary>
    /// <seealso cref="Store.Domain.Core.Repositories.IRepository{Store.Domain.Core.Entities.Product}" />
    public class ProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;BaseEntity&gt;.</returns>
        public IEnumerable<Product> GetAll()
        {
            return StoreContext.Products;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>BaseEntity.</returns>
        public Product GetById(string id)
        {
            return StoreContext.Products.Find(p =>p.Id == id);
        }

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;Product&gt;.</returns>
        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return StoreContext.Products.Where(predicate).ToList();
        }


        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public bool Create(Product entity)
        {
            StoreContext.Products.Add(entity);

            return StoreContext.Products.FirstOrDefault(p => p.Id == entity.Id) != null;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public bool Update(Product entity)
        {
            var toUpdate = StoreContext.Products.FirstOrDefault(p => p.Id == entity.Id);
            if (toUpdate is null)
                return false;

            toUpdate = entity;

            return StoreContext.Products.Contains(toUpdate);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public bool Delete(string id)
        {
            StoreContext.Products.RemoveAll(p => p.Id == id);

            return StoreContext.Products.FirstOrDefault(p => p.Id == id) == null;
        }     
    }
}
