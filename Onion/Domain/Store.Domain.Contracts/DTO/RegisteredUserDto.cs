using System.Collections.Generic;

namespace Store.Domain.Contracts.DTO
{
    /// <summary>
    /// Class RegisteredUserDto, used for working with services.
    /// </summary>
    public class RegisteredUserDto: BaseEntityDto
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>The type of the user.</value>
        public string UserType { get; set; }
        /// <summary>
        /// Gets or sets the credentials identifier.
        /// </summary>
        /// <value>The credentials identifier.</value>
        public string CredentialsId { get; set; }
        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>The orders.</value>
        public List<OrderDto> Orders { get; set; }
    }
}
