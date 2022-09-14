using Store.DAL.Enums;

namespace Store.DAL.Users
{
    public class Guest : User
    {
        public Guest(UserType type = UserType.Guest) : base(type)
        {

        }
    }
}
