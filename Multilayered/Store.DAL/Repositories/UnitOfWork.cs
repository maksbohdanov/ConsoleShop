using Store.DAL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Users;

namespace Store.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {      
        private ProductRepository productRepository;
        private OrderRepository orderRepository;
        private UserRepository userRepository;        

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository();
                return productRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository();
                return orderRepository;
            }
        }

        public IRepository<RegisteredUser> Users
        {
            get
            {
                if(userRepository == null)
                    userRepository = new UserRepository();
                return userRepository;
            }
        }
    }
}
