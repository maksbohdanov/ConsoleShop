using Store.BLL.DTO;

namespace Store.ConsoleApp.FormatExtensions
{
    public static class ProductFormat
    {
        public static string Format(this ProductDto product, bool withAmount = false)
        {
            var result = $"Name : {product.Name}" +
                $"|\tCaregory : {product.Category}" +
                $"|\tDescription : {product.Description}" +
                $"|\tCost : {product.Cost}";
            return withAmount ? result + $"|\tAmount : {product.Amount}\n" : result;
        }
    }
}
