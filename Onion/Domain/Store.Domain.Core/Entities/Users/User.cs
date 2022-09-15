using Store.Domain.Core.Enums;

namespace Store.Domain.Core.Entities
{
    /// <summary>
    /// Abstract class User used for creating users.
    /// </summary>
    public abstract class User: BaseEntity
    {
        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>The type of the user.</value>
        public UserType UserType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        protected User(UserType type)
        {
            UserType = type;
        }
    }
}
