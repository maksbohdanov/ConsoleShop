using System;
using System.Collections.Generic;
using Store.Services.Abstractions;
using Store.Domain.Core.Repositories;
using Store.Domain.Core.Exceptions;
using Store.Domain.Core.Entities;
using Store.Domain.Contracts.Extensions;
using Store.Domain.Contracts.DTO;
using System.Linq;

namespace Store.Services
{
    /// <summary>
    /// Class ProductService.
    /// Implements the <see cref="IProductService" />
    /// </summary>
    /// <seealso cref="IProductService" />
    public class ProductService : IProductService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;


        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public ProductService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        /// Gets the product by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ProductDto.</returns>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">Specified product was not found.</exception>
        public ProductDto GetProduct(string id)
        {
            var result = _unitOfWork.Products.GetById(id);

            if (result == null)
            {
                throw new NotFoundException("Specified product was not found.");
            }
            return result.ToDto();
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>IEnumerable&lt;ProductDto&gt;.</returns>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">There are no products yet.</exception>
        public IEnumerable<ProductDto> GetAllProducts()
        {
            var result = _unitOfWork.Products.GetAll();
            if (result == null)
            {
                throw new NotFoundException("There are no products yet.");
            }
            return result.Select(p => p.ToDto());
        }

        /// <summary>
        /// Finds the product by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>ProductDto.</returns>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">Product with specified name was not found.</exception>
        public ProductDto FindProduct(string name)
        {
            var result = _unitOfWork.Products.Find(p => p.Name.ToLower() == name.ToLower()).FirstOrDefault();

            if (result == null)
            {
                throw new NotFoundException("Product with specified name was not found.");
            }
            return result.ToDto();
        }

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="productDto">The product dto.</param>
        /// <exception cref="Store.Domain.Core.Exceptions.ProductAlreadyExistsException">Product with such name already exists.</exception>
        public bool CreateProduct(ProductDto productDto)
        {
            if (productDto == null)
            {
                return false;
            }

            if (ProductExists(productDto.Name))
            {
                throw new ProductAlreadyExistsException("Product with such name already exists.");
            }

            var product = new Product(productDto.Name, productDto.Category, productDto.Description, productDto.Cost);
            return _unitOfWork.Products.Create(product);                
        }

        /// <summary>
        /// Changes the product.
        /// </summary>
        /// <param name="productDto">The product dto.</param>
        /// <exception cref="Store.Domain.Core.Exceptions.NotFoundException">Specified product was not found.</exception>
        public bool ChangeProduct(ProductDto productDto)
        {
            var product = _unitOfWork.Products.GetById(productDto.Id);
            if (product == null)
            {
                throw new NotFoundException("Specified product was not found.");
            }
            product.Name = productDto.Name;
            product.Category = productDto.Category;
            product.Description = productDto.Description;
            product.Cost = productDto.Cost;

            return _unitOfWork.Products.Update(product);
        }

        private bool ProductExists(string name)
        {
            var products = _unitOfWork.Products.Find(p => p.Name.ToLower() == name.ToLower());               

            return products.Any(p => p.Name == name);
        }
    }
}
