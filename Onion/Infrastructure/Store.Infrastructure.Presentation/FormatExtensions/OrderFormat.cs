using Store.Domain.Contracts.DTO;
using System.Text;

namespace Store.Infrastructure.Presentation.FormatExtensions
{
    /// <summary>
    /// Class OrderFormat.
    /// </summary>
    public static class OrderFormat
    {
        /// <summary>
        /// Formats the specified order output.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns>System.String.</returns>
        public static string Format(this OrderDto order)
        {
            var result = new StringBuilder($"Id: {order.Id}\tStatus :{order.Status}\nProducts:\n");

            foreach (var product in order.Products)
            {
                result.Append(product.Format(true));
            }
            return result.ToString();
        }
    }
}
