using System.Collections.Generic;

namespace Store.BLL.DTO
{
    public class OrderDto: BaseEntityDto
    {
        public string Status { get; set; }
        public string UserId { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
