using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProductServiceTests
    {
        private IProductService _productService;
        private Mock<IUnitOfWork> _unitOfWork;
        private TestDataRepository _dataRepository;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _productService = new ProductService(_unitOfWork.Object);
            _dataRepository = new TestDataRepository();
        }

        [TestCase]
        public void GetProductById_WorksCorrectly()
        {
            var productId = Guid.NewGuid().ToString("N")[..6];
            var expected = _dataRepository.Products[0];
            expected.Id = productId;
            _unitOfWork.Setup(x => x.Products.GetById(productId))
                .Returns(_dataRepository.Products[0]);

            var actual = _productService.GetProduct(productId);

            Assert.That(actual, Is.EqualTo(expected.ToDto()).Using(new ProductComparer()));
        }

        [TestCase]
        public void GetProductById_ThrowsNotFoundException()
        {
            _unitOfWork.Setup(x => x.Products.GetById(It.IsAny<string>()))
                .Returns(() => null);

            Assert.Throws<NotFoundException>(() => _productService.GetProduct(It.IsAny<string>()));
        }

        [TestCase]
        public void GetProductByName_WorksCorrectly()
        {
            var productName = "banana";
            var expected = _dataRepository.Products[2];

            _unitOfWork.Setup(x => x.Products.Find(It.IsAny<Func<Product, bool>>()))
                .Returns(_dataRepository.Products
                    .Where(p => p.Name.ToLower() == productName.ToLower()));

            var actual = _productService.FindProduct(productName);

            Assert.That(actual, Is.EqualTo(expected.ToDto()).Using(new ProductComparer()));
        }

        [TestCase]
        public void GetProductByName_ThrowsNotFoundException()
        {
            _unitOfWork.Setup(x => x.Products.Find(It.IsAny<Func<Product, bool>>()))
                .Returns(new List<Product>());

            Assert.Throws<NotFoundException>(() => _productService.FindProduct(It.IsAny<string>()));
        }

        [TestCase]
        public void GetAllProducts_WorksCorrectly()
        {
            _unitOfWork.Setup(x => x.Products.GetAll())
                .Returns(_dataRepository.Products);

            _productService.GetAllProducts();

            _unitOfWork.Verify(x => x.Products.GetAll(), Times.Once());
        }

        [TestCase]
        public void GetAllProducts_ThrowsNotFoundException()
        {
            _unitOfWork.Setup(x => x.Products.GetAll())
                .Returns(() => null);

            Assert.Throws<NotFoundException>(() => _productService.GetAllProducts());
        }

        [TestCase]
        public void CreateProduct_AddsValueToDatabase()
        {
            var product = new Product("A", "a", "A", 5);
            _unitOfWork.Setup(x => x.Products.Create(It.IsAny<Product>()))
                .Returns(true);

            _productService.CreateProduct(product.ToDto());

            _unitOfWork.Verify(x => x.Products.Create(It.IsAny<Product>()), Times.Once());
        }

        [TestCase]
        public void CreateProduct_ReturnsFalse_WhenProductIsNull()
        {
            var expected = false;

            var actual = _productService.CreateProduct(null);

            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void CreateProduct_ThrowsProductAlreadyExistsException()
        {
            var product = new Product("Fanta", "a", "A", 5);
            _unitOfWork.Setup(x => x.Products.Create(It.IsAny<Product>()))
                .Returns(true);
            _unitOfWork.Setup(x => x.Products.Find(It.IsAny<Func<Product, bool>>()))
                .Returns(_dataRepository.Products);

            Assert.Throws<ProductAlreadyExistsException>(() => _productService.CreateProduct(product.ToDto()));
        }

        [TestCase]
        public void ChangeProduct_UpdatesProduct()
        {
            var product = _dataRepository.Products[0];
            _unitOfWork.Setup(x => x.Products.Update(It.IsAny<Product>()))
                .Returns(true);
            _unitOfWork.Setup(x => x.Products.GetById(It.IsAny<string>()))
                .Returns(_dataRepository.Products[0]);

            _productService.ChangeProduct(product.ToDto());

            _unitOfWork.Verify(x => x.Products.Update(It.IsAny<Product>()), Times.Once());
        }

        [TestCase]
        public void ChangeProduct_ThrowsNotFoundException()
        {
            var product = _dataRepository.Products[0];
            _unitOfWork.Setup(x => x.Products.Update(It.IsAny<Product>()))
                .Returns(true);
            _unitOfWork.Setup(x => x.Products.GetById(It.IsAny<string>()))
                .Returns(() => null);

            Assert.Throws<NotFoundException>(() => _productService.ChangeProduct(product.ToDto()));
        }
    }
}
