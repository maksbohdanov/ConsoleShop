using Store.DAL.Entities;
using System;

namespace Store.DAL.Users
{
    public class Credentials: BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Credentials(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public override bool Equals(object obj)
        {
            if (obj is Credentials user)
            {
                return Id == user.Id &&
                    Email == user.Email &&
                    Password == user.Password;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Email, Password);
        }
    }
}
