using System;

namespace Store.Domain.Core.Entities
{
    /// <summary>
    /// Class BaseEntity, has only one key Id.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// </summary>
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString("N")[..6];
        }
    }
}
