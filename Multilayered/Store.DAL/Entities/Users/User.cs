using Store.DAL.Entities;
using Store.DAL.Enums;

namespace Store.DAL.Users
{
    public abstract class User: BaseEntity
    {
        public UserType UserType { get; set; }

        protected User(UserType type)
        {
            UserType = type;
        }
    }
}
