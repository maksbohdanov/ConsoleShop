using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Interfaces;
using Store.BLL.Extensions;
using System.Collections.Generic;
using System.Linq;
using Store.BLL.Exceptions;
using Store.DAL.Entities;

namespace Store.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ProductService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public ProductDto GetProduct(string id)
        {
            var result = _unitOfWork.Products.GetById(id);

            if (result == null)
            {
                throw new NotFoundException("Specified product was not found.");
            }
            return result.ToDto();
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var result = _unitOfWork.Products.GetAll();
            if (result == null)
            {
                throw new NotFoundException("There are no products yet.");
            }
            return result.Select(p => p.ToDto());
        }

        public ProductDto FindProduct(string name)
        {
            var result = _unitOfWork.Products.Find(p => p.Name.ToLower() == name.ToLower()).FirstOrDefault();

            if (result == null)
            {
                throw new NotFoundException("Product with specified name was not found.");
            }
            return result.ToDto();
        }
          
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
