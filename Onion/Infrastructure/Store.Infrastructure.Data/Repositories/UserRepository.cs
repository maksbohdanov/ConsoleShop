using System;
using System.Collections.Generic;
using System.Linq;
using Store.Domain.Core.Entities;
using Store.Domain.Core.Repositories;

namespace Store.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository for working with users.
    /// Implements the <see cref="Store.Domain.Core.Repositories.IRepository{Store.Domain.Core.Entities.RegisteredUser}" />
    /// </summary>
    /// <seealso cref="Store.Domain.Core.Repositories.IRepository{Store.Domain.Core.Entities.RegisteredUser}" />
    public class UserRepository: IRepository<RegisteredUser>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;BaseEntity&gt;.</returns>
        public IEnumerable<RegisteredUser> GetAll()
        {
            return StoreContext.Users;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>BaseEntity.</returns>
        public RegisteredUser GetById(string id)
        {
            return StoreContext.Users.Find(p => p.Id == id);
        }

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;RegisteredUser&gt;.</returns>
        public IEnumerable<RegisteredUser> Find(Func<RegisteredUser, bool> predicate)
        {
            return StoreContext.Users.Where(predicate).ToList();
        }


        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public bool Create(RegisteredUser entity)
        {
            StoreContext.Users.Add(entity);

            return StoreContext.Users.FirstOrDefault(p => p.Id == entity.Id) != null;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public bool Update(RegisteredUser entity)
        {
            var toUpdate = StoreContext.Users.FirstOrDefault(p => p.Id == entity.Id);
            if (toUpdate is null)
                return false;

            toUpdate = entity;

            return StoreContext.Users.Contains(toUpdate);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public bool Delete(string id)
        {
            StoreContext.Users.RemoveAll(p => p.Id == id);

            return StoreContext.Users.FirstOrDefault(p => p.Id == id) == null;
        }
    }
}
