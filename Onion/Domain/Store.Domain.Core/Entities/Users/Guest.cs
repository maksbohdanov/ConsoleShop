using Store.Domain.Core.Enums;

namespace Store.Domain.Core.Entities
{
    /// <summary>
    /// Class Guest used for creating guests.
    /// </summary>
    public class Guest : User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Guest"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Guest(UserType type = UserType.Guest) : base(type)
        {

        }
    }
}
