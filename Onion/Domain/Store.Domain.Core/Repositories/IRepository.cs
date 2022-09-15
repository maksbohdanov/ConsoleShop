using System;
using System.Collections.Generic;

namespace Store.Domain.Core.Repositories
{
    /// <summary>
    /// Basic repository(CRUD): create, read, update, delete
    /// </summary>
    /// <typeparam name="BaseEntity">The type of the base entity.</typeparam>
    public interface IRepository<BaseEntity> where BaseEntity: class
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;BaseEntity&gt;.</returns>
        IEnumerable<BaseEntity> GetAll();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>BaseEntity.</returns>
        BaseEntity GetById(string id);

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        IEnumerable<BaseEntity> Find(Func<BaseEntity, Boolean> predicate);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        bool Create(BaseEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        bool Update(BaseEntity entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        bool Delete(string id);
    }
}
