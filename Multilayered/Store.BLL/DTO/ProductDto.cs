namespace Store.BLL.DTO
{
    public class ProductDto: BaseEntityDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Amount { get; set; }
    }
}
