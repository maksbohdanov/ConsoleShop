using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Store.Tests.EqualityComparers;
using Store.Services.Abstractions;
using Store.Services;
using Store.Domain.Core.Repositories;
using Store.Domain.Contracts.Extensions;
using Store.Domain.Core.Exceptions;
using Store.Domain.Core.Entities;
using Store.Domain.Contracts.DTO;

namespace Store.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _userService;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IOrderService> _orderService;
        private Mock<IProductService> _productService;
        private TestDataRepository _dataRepository;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _orderService = new Mock<IOrderService>();
            _productService = new Mock<IProductService>();
            _userService = new UserService(_unitOfWork.Object, _orderService.Object, _productService.Object);
            _dataRepository = new TestDataRepository();
        }

        [TestCase]
        public void GetUserById_WorksCorrectly()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            var expected = _dataRepository.Users[0];
            expected.Id = userId;
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[0]);

            var actual = _userService.GetUser(userId);

            Assert.That(actual, Is.EqualTo(expected.ToDto()).Using(new RegisteredUserComparer()));
        }

        [TestCase]
        public void GetUserById_ThrowsNotFoundException()
        {
            _unitOfWork.Setup(x => x.Users.GetById(It.IsAny<string>()))
                .Returns(() => null);

            Assert.Throws<NotFoundException>(() => _userService.GetUser(It.IsAny<string>()));
        }

        [TestCase]
        public void GetUserByEmail_WorksCorrectly()
        {
            var userEmail = "mtaylor@mail.com";
            var expected = _dataRepository.Users[4];

            _unitOfWork.Setup(x => x.Users.Find(It.IsAny<Func<RegisteredUser, bool>>()))
                .Returns(_dataRepository.Users
                    .Where(p => p.Credentials.Email == userEmail));

            var actual = _userService.FindUser(userEmail);

            Assert.That(actual, Is.EqualTo(expected.ToDto()).Using(new RegisteredUserComparer()));
        }

        [TestCase]
        public void GetUserByEmail_ThrowsNotFoundException()
        {
            _unitOfWork.Setup(x => x.Users.Find(It.IsAny<Func<RegisteredUser, bool>>()))
                .Returns(new List<RegisteredUser>());

            Assert.Throws<NotFoundException>(() => _userService.FindUser(It.IsAny<string>()));
        }

        [TestCase]
        public void GetAllUsers_WorksCorrectly()
        {
            _unitOfWork.Setup(x => x.Users.GetAll())
                .Returns(_dataRepository.Users);

            _userService.GetAllUsers();

            _unitOfWork.Verify(x => x.Users.GetAll(), Times.Once());
        }

        [TestCase]
        public void GetAllUsers_ThrowsNotFoundException()
        {
            _unitOfWork.Setup(x => x.Users.GetAll())
                .Returns(() => null);

            Assert.Throws<NotFoundException>(() => _userService.GetAllUsers());
        }

        [TestCase]
        public void CreateUser_AddsValueToDatabase()
        {
            var user = new UserModel
            {
                FirstName = "Alex",
                LastName = "Smith",
                Email = "a.smith@mail.com",
                Password = "passw0rd"
            };
            _unitOfWork.Setup(x => x.Users.Create(It.IsAny<RegisteredUser>()))
                .Returns(true);

            _userService.CreateUser(user);

            _unitOfWork.Verify(x => x.Users.Create(It.IsAny<RegisteredUser>()), Times.Once());
        }

        [TestCase]
        public void CreateUser_ThrowsEmailAlreadyTakenException()
        {
            var user = new UserModel
            {
                FirstName = "Travis",
                LastName = "Scott",
                Email = "tscott@mail.com",
                Password = "passw0rd"
            };
            _unitOfWork.Setup(x => x.Users.Create(It.IsAny<RegisteredUser>()))
                .Returns(true);
            _unitOfWork.Setup(x => x.Users.Find(It.IsAny<Func<RegisteredUser, bool>>()))
                .Returns(_dataRepository.Users);

            Assert.Throws<EmailAlreadyTakenException>(() => _userService.CreateUser(user));
        }

        [TestCase]
        public void ChangeUser_UpdatesUser()
        {
            var user = new UserModel
            {
                FirstName = "Alex",
                LastName = "Smith",
                Email = "a.smith@mail.com",
                Password = "passw0rd"
            };
            var userId = _dataRepository.Users[1].Id;
            _unitOfWork.Setup(x => x.Users.Update(It.IsAny<RegisteredUser>()))
                .Returns(true);
            _unitOfWork.Setup(x => x.Users.GetById(It.IsAny<string>()))
                .Returns(_dataRepository.Users[1]);

            _userService.ChangeUser(userId, user);

            _unitOfWork.Verify(x => x.Users.Update(It.IsAny<RegisteredUser>()), Times.Once());
        }

        [TestCase]
        public void ChangeUser_ThrowsNotFoundException()
        {
            var user = new UserModel
            {
                FirstName = "Alex",
                LastName = "Smith",
                Email = "a.smith@mail.com",
                Password = "passw0rd"
            };
            var userId = _dataRepository.Users[1].Id;
            _unitOfWork.Setup(x => x.Users.Update(It.IsAny<RegisteredUser>()))
                .Returns(true);
            _unitOfWork.Setup(x => x.Users.GetById(It.IsAny<string>()))
                .Returns(() => null);

            Assert.Throws<NotFoundException>(() => _userService.ChangeUser(userId, user));
        }

        [TestCase]
        public void ChangeUser_EmailAlreadyTakenException()
        {
            var user = new UserModel
            {
                FirstName = "Travis",
                LastName = "Scott",
                Email = "tscott@mail.com",
                Password = "passw0rd"
            };
            var userId = _dataRepository.Users[1].Id;
            _unitOfWork.Setup(x => x.Users.Update(It.IsAny<RegisteredUser>()))
                .Returns(true);
            _unitOfWork.Setup(x => x.Users.Find(It.IsAny<Func<RegisteredUser, bool>>()))
                .Returns(_dataRepository.Users);

            Assert.Throws<EmailAlreadyTakenException>(() => _userService.ChangeUser(userId, user));
        }

        [TestCase]
        public void AddNewOrder_AddsOrder()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            var orderId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[0]);

            _unitOfWork.Setup(x => x.Users.Update(It.IsAny<RegisteredUser>()))
                .Returns(true);

            _orderService.Setup(x => x.CreateOrder())
                .Returns(orderId);

            _unitOfWork.Setup(x => x.Orders.GetById(orderId))
                .Returns(() => new Order());

            _userService.AddNewOrder(userId);

            _unitOfWork.Verify(x => x.Users.Update(It.IsAny<RegisteredUser>()), Times.Once());
        }

        [TestCase]
        public void AddToOrder_AddsProductToOrder()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            var productId = Guid.NewGuid().ToString("N")[..6];
            var productName = "Fanta";
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[2]);

            _unitOfWork.Setup(x => x.Users.Update(It.IsAny<RegisteredUser>()))
                .Returns(true);

            _productService.Setup(x => x.FindProduct(productName))
                .Returns(_dataRepository.Products[0].ToDto());

            _unitOfWork.Setup(x => x.Products.GetById(It.IsAny<string>()))
                .Returns(_dataRepository.Products[0]);

            _userService.AddToOrder(userId, productName, It.IsAny<int>());

            _unitOfWork.Verify(x => x.Users.Update(It.IsAny<RegisteredUser>()), Times.Once());
        }

        [TestCase]
        public void AddToOrder_ThrowsNoOrdersException()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            var productId = Guid.NewGuid().ToString("N")[..6];
            var productName = "Fanta";
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[0]);

            _unitOfWork.Setup(x => x.Users.Update(It.IsAny<RegisteredUser>()))
                .Returns(true);

            Assert.Throws<NoOrdersException>(() => _userService.AddToOrder(userId, productName, It.IsAny<int>()));
        }

        [TestCase]
        public void AddToOrder_ThrowsWrongStatusException()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            var productId = Guid.NewGuid().ToString("N")[..6];
            var productName = "Fanta";
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[1]);

            _unitOfWork.Setup(x => x.Users.Update(It.IsAny<RegisteredUser>()))
                .Returns(true);

            Assert.Throws<WrongStatusException>(() => _userService.AddToOrder(userId, productName, It.IsAny<int>()));
        }

        [TestCase]
        public void CancelOrder_CancelsOrder()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[2]);

            _orderService.Setup(x => x.ChangeStatus(It.IsAny<string>(), "CanceledByUser"))
                .Returns(true);

            _userService.CancelOrder(userId);

            _orderService.Verify(x => x.ChangeStatus(It.IsAny<string>(), "CanceledByUser"), Times.Once());
        }

        [TestCase]
        public void CancelOrder_ThrowsNoOrdersException()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[0]);

            _orderService.Setup(x => x.ChangeStatus(It.IsAny<string>(), "CanceledByUser"))
                .Returns(true);

            Assert.Throws<NoOrdersException>(() => _userService.CancelOrder(userId));            
        }

        [TestCase]
        public void CancelOrder_ThrowsWrongStatusException()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[1]);

            _orderService.Setup(x => x.ChangeStatus(It.IsAny<string>(), "CanceledByUser"))
                .Returns(true);

            Assert.Throws<WrongStatusException>(() => _userService.CancelOrder(userId));
        }

        [TestCase]
        public void ReceiveOrder_ReceivesOrder()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[2]);

            _orderService.Setup(x => x.ChangeStatus(It.IsAny<string>(), "Received"))
                .Returns(true);

            _userService.ReceiveOrder(userId);

            _orderService.Verify(x => x.ChangeStatus(It.IsAny<string>(), "Received"), Times.Once());
        }

        [TestCase]
        public void ReceiveOrder_ThrowsNoOrdersException()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[0]);

            _orderService.Setup(x => x.ChangeStatus(It.IsAny<string>(), "CanceledByUser"))
                .Returns(true);

            Assert.Throws<NoOrdersException>(() => _userService.ReceiveOrder(userId));
        }

        [TestCase]
        public void ReceiveOrder_ThrowsWrongStatusException()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[1]);

            _orderService.Setup(x => x.ChangeStatus(It.IsAny<string>(), "CanceledByUser"))
                .Returns(true);

            Assert.Throws<WrongStatusException>(() => _userService.ReceiveOrder(userId));
        }

        [TestCase]
        public void GetUserOrders_WorksCorrectly()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[1]);

            var orders = _userService.GetUserOrders(userId);

            Assert.NotNull(orders);            
        }

        [TestCase]
        public void GetUserOrders_ThrowsNotFoundException()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Users.GetById(userId))
                .Returns(_dataRepository.Users[0]);

            Assert.Throws<NotFoundException>(() => _userService.GetUserOrders(userId));
        }

        [TestCase]
        public void LogIn_WorksCorrectly()
        {
            var email = "tscott@mail.com";
            var password = "5";
            var expected = _dataRepository.Users[5].ToDto();
            _unitOfWork.Setup(x => x.Users.Find(It.IsAny<Func<RegisteredUser, bool>>()))
                .Returns(_dataRepository.Users
                .Where(x => x.Credentials.Email == email));

            var actual = _userService.LogIn(email, password);

            Assert.That(actual, Is.EqualTo(expected).Using(new RegisteredUserComparer()));
        }

        [TestCase]
        public void LogIn_ThrowsNotFoundException()
        {
            var email = "tscott@mail.com";
            var password = "5";
            _unitOfWork.Setup(x => x.Users.Find(It.IsAny<Func<RegisteredUser, bool>>()))
                .Returns(() => new List<RegisteredUser>());           

            Assert.Throws<NotFoundException>(() => _userService.LogIn(email, password));
        }

        [TestCase]
        public void LogIn_ThrowsWrongPasswordException()
        {
            var email = "tscott@mail.com";
            var password = "qqqqq";
            _unitOfWork.Setup(x => x.Users.Find(It.IsAny<Func<RegisteredUser, bool>>()))
               .Returns(_dataRepository.Users
               .Where(x => x.Credentials.Email == email));

            Assert.Throws<WrongPasswordException>(() => _userService.LogIn(email, password));
        }
    }
}
