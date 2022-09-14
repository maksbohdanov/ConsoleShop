using Store.BLL.DTO;
using System.Collections.Generic;

namespace Store.BLL.Interfaces
{
    public interface IProductService
    {
        ProductDto GetProduct(string id);
        IEnumerable<ProductDto> GetAllProducts();
        ProductDto FindProduct(string name);
        bool CreateProduct(ProductDto productDto);        
        bool ChangeProduct(ProductDto productDto);
    }
}
