namespace Store.DAL.Entities
{
    public class OrderDetails
    {
        public int Amount { get; set; }
        public Product Product { get; set; }
       
        public OrderDetails(int amount, Product product)
        {
            Amount = amount;
            Product = product;
        }
    }
}
