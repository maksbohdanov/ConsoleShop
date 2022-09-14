using Store.BLL.DTO;
using System.Text;

namespace Store.ConsoleApp.FormatExtensions
{
    public static class UserFormat
    {
        public static string Format(this RegisteredUserDto user)
        {
            var result = new StringBuilder($"First Name : {user.FirstName}" +
                $"|\tLast Name : {user.LastName}" +
                $"|\nUser Type : {user.UserType}" +
                "\n\nOrders:\n");
            foreach (var order in user.Orders)
            {
                result.Append(order.Format());
            }
            return result.ToString();
        }
    }
}
