using System;
using NUnit.Framework;
using Moq;
using Store.BLL.Services;
using Store.BLL.Interfaces;
using Store.DAL.Interfaces;
using Store.DAL.Entities;
using Store.BLL.Extensions;
using Store.Tests.EqualityComparers;
using Store.BLL.Exceptions;

namespace Store.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private IOrderService _orderService;
        private Mock<IUnitOfWork> _unitOfWork;
        private TestDataRepository _dataRepository;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _orderService = new OrderService(_unitOfWork.Object);
            _dataRepository = new TestDataRepository();
        }

        [TestCase]
        public void GetOrderById_WorksCorrectly()
        {
            var orderId = Guid.NewGuid().ToString("N")[..6];
            var userId = Guid.NewGuid().ToString("N")[..6];
            var expected = _dataRepository.Orders[0];
            expected.Id = orderId;
            _unitOfWork.Setup(x => x.Orders.GetById(orderId))
                .Returns(_dataRepository.Orders[0]);

            var actual = _orderService.GetOrder(orderId, userId);

            Assert.That(actual, Is.EqualTo(expected.ToDto(userId)).Using(new OrderComparer()));
        }

        [TestCase]
        public void GetOrderById_ThrowsNotFoundException()
        {
            _unitOfWork.Setup(x => x.Orders.GetById(It.IsAny<string>()))
                .Returns(() => null);

            Assert.Throws<NotFoundException>(() => _orderService.GetOrder(It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestCase]
        public void GetAllOrders_WorksCorrectly()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Orders.GetAll())
                .Returns(_dataRepository.Orders);

            _orderService.GetAllOrders(userId);

            _unitOfWork.Verify(x => x.Orders.GetAll(), Times.Once());
        }

        [TestCase]
        public void GetAllOrders_ThrowsNotFoundException()
        {
            var userId = Guid.NewGuid().ToString("N")[..6];
            _unitOfWork.Setup(x => x.Orders.GetAll())
                .Returns(() => null);

            Assert.Throws<NotFoundException>(() => _orderService.GetAllOrders(userId));
        }

        [TestCase]
        public void CreateOrder_AddsValueToDatabase()
        {
            _unitOfWork.Setup(x => x.Orders.Create(It.IsAny<Order>()))
                .Returns(true);

            _orderService.CreateOrder();

            _unitOfWork.Verify(x => x.Orders.Create(It.IsAny<Order>()), Times.Once());
        }

        [TestCase]
        public void CreateOrder_ThrowsorderAlreadyExistsException()
        {
            _unitOfWork.Setup(x => x.Orders.Create(It.IsAny<Order>()))
                .Returns(false);

            Assert.Throws<OrderCreationException>(() => _orderService.CreateOrder());
        }

        [TestCase]
        public void Changeorder_UpdatesOrder()
        {
            var order = _dataRepository.Orders[0];
            var status = "Received";
            _unitOfWork.Setup(x => x.Orders.Update(It.IsAny<Order>()))
                .Returns(true);
            _unitOfWork.Setup(x => x.Orders.GetById(It.IsAny<string>()))
                .Returns(_dataRepository.Orders[0]);

            _orderService.ChangeStatus(order.Id, status);

            _unitOfWork.Verify(x => x.Orders.Update(It.IsAny<Order>()), Times.Once());
        }

        [TestCase]
        public void ChangeOrder_ThrowsNotFoundException()
        {
            var order = _dataRepository.Orders[0];
            var status = "Received";
            _unitOfWork.Setup(x => x.Orders.Update(It.IsAny<Order>()))
                .Returns(true);
            _unitOfWork.Setup(x => x.Orders.GetById(It.IsAny<string>()))
                .Returns(() => null);

            Assert.Throws<NotFoundException>(() => _orderService.ChangeStatus(order.Id, status));
        }
    }
}
