using Store.Domain.Contracts.DTO;

namespace Store.Infrastructure.Presentation.FormatExtensions
{
    /// <summary>
    /// Class ProductFormat.
    /// </summary>
    public static class ProductFormat
    {
        /// <summary>
        /// Formats the specified product output.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="withAmount">if set to <c>true</c> [with amount].</param>
        /// <returns>System.String.</returns>
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
