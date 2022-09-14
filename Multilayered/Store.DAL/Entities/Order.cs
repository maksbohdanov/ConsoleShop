using Store.DAL.Enums;
using System.Collections.Generic;

namespace Store.DAL.Entities
{
    public class Order: BaseEntity
    {       
        public List<OrderDetails> Products { get; set; }
        public StatusType Status { get; set; }

        public Order()
        {            
            Status = StatusType.New;
            Products = new List<OrderDetails>();
        }
        public Order(List<OrderDetails> products) : this()
        {
            Products = products;
        }
    }
}
