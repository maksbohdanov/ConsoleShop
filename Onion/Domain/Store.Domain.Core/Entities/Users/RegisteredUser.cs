using Store.Domain.Core.Enums;
using System;
using System.Collections.Generic;

namespace Store.Domain.Core.Entities
{
    /// <summary>
    /// Class RegisteredUser used for creating registered users.
    /// </summary>
    /// <seealso cref="Store.Domain.Core.Entities.User" />
    public class RegisteredUser:User
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
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        public Credentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>The orders.</value>
        public List<Order> Orders { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredUser"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        private RegisteredUser(UserType type) : base(type)
        {
            Orders = new List<Order>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredUser"/> class.
        /// </summary>
        /// <param name="fname">The First Name.</param>
        /// <param name="lname">The Last Name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="type">The type.</param>
        public RegisteredUser(string fname, string lname, string email, string password, UserType type)
         : this(type)
        {
            FirstName = fname;
            LastName = lname;
            Credentials = new Credentials(email, password);            
        }
    }
}
