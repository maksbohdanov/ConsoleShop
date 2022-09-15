namespace Store.Domain.Core.Entities
{
    /// <summary>
    /// Class Credentials used for creating credentials.
    /// </summary>
    public class Credentials: BaseEntity
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        public Credentials(string email, string password)
        {
            Email = email;
            Password = password;
        }        
    }
}
