using Store.BLL.DTO;
using System.Text;

namespace Store.ConsoleApp.FormatExtensions
{
    public static class OrderFormat
    {
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
