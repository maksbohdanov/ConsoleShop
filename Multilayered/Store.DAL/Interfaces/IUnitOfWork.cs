using Store.DAL.Entities;
using Store.DAL.Users;

namespace Store.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        IRepository<RegisteredUser> Users { get; }
    }
}
