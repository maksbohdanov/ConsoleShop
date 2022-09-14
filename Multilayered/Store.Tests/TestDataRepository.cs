using Store.DAL.Entities;
using Store.DAL.Users;
using Store.DAL.Enums;
using System.Collections.Generic;

namespace Store.Tests
{
    public class TestDataRepository
    {
        private List<Product> products;
        private List<Order> orders;
        private List<RegisteredUser> users;

        public TestDataRepository()
        {
            products = new List<Product>()
            {
                new Product("Fanta", "Water", "Lorem ipsum", 20),
                new Product("7up", "Water", "Lorem ipsum", 19),
                new Product("Banana", "Fruits", "Lorem ipsum", 15),
                new Product("Apple", "Fruits", "Lorem ipsum", 12),
                new Product("Orange", "Fruits", "Lorem ipsum", 18),
                new Product("Potato", "Vegetables", "Lorem ipsum", 5),
                new Product("Carrot", "Vegetables", "Lorem ipsum", 7),
                new Product("Onion", "Vegetables", "Lorem ipsum", 6),
            };

            orders = new List<Order>()
            {
                new Order(new List<OrderDetails>()
                {
                    new OrderDetails(2, Products[0]),
                    new OrderDetails(5, Products[1]),
                    new OrderDetails(12, Products[2]),
                    new OrderDetails(3, Products[6])
                }),
                new Order(new List<OrderDetails>()
                {
                    new OrderDetails(8, Products[7]),
                    new OrderDetails(5, Products[4])
                }){Status = StatusType.Completed},
                new Order(new List<OrderDetails>()
                {
                    new OrderDetails(1, Products[5]),
                    new OrderDetails(1, Products[0])
                }),
                new Order(new List<OrderDetails>()
                {
                    new OrderDetails(10, Products[1]),
                    new OrderDetails(20, Products[2]),
                    new OrderDetails(513, Products[4])
                }),
                new Order(new List<OrderDetails>()
                {
                    new OrderDetails(5, Products[6]),
                    new OrderDetails(65, Products[5]),
                    new OrderDetails(11, Products[3]),
                    new OrderDetails(12, Products[2])
                }),
                new Order(new List<OrderDetails>()
                {
                    new OrderDetails(7, Products[0])
                })
            };

            users = new List<RegisteredUser>()
            {
                new RegisteredUser("admin", "admin", "admin", "admin", UserType.Administrator ),
                new RegisteredUser("Peter", "Green", "pgreen@mail.com", "1", UserType.RegisteredUser)
                    { Orders = new List<Order>() { Orders[0], Orders[1] } },
                new RegisteredUser("Mark", "Smith", "msith@mail.com", "2", UserType.RegisteredUser)
                    { Orders = new List<Order>() { Orders[2] } },
                new RegisteredUser("John", "Jones", "jjones@mail.com", "3", UserType.RegisteredUser)
                    { Orders = new List<Order>() { Orders[3] } },
                new RegisteredUser("Mike", "Taylor", "mtaylor@mail.com", "4", UserType.RegisteredUser)
                     { Orders = new List<Order>() { Orders[4] } },
                new RegisteredUser("Travis", "Scott", "tscott@mail.com", "5", UserType.RegisteredUser)
                    { Orders = new List<Order>() { Orders[5] } },
                new RegisteredUser("1", "1", "1", "1", UserType.RegisteredUser)
            };

        }

        public List<Product> Products
        {
            get { return products; }
            set { products = value; }
        }
        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }
        public List<RegisteredUser> Users
        {
            get { return users; }
            set { users = value; }
        }
    }
}
