using Store.DAL.Entities;
using Store.DAL.Enums;
using System;
using System.Collections.Generic;

namespace Store.DAL.Users
{
    public class RegisteredUser:User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Credentials Credentials { get; set; }
        public List<Order> Orders { get; set; }

        private RegisteredUser(UserType type) : base(type)
        {
            Orders = new List<Order>();
        }

        public RegisteredUser(string fname, string lname, string email, string password, UserType type)
         : this(type)
        {
            FirstName = fname;
            LastName = lname;
            Credentials = new Credentials(email, password);            
        }

        public override bool Equals(object obj)
        {
            if (obj is RegisteredUser user)
            {
                return Id == user.Id &&
                    FirstName == user.FirstName &&
                    LastName == user.LastName &&
                    Credentials.Equals(user.Credentials) &&
                    UserType == user.UserType;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Credentials, UserType);
        }
    }
}
