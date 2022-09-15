using Store.Domain.Contracts.DTO;
using System.Text;

namespace Store.Infrastructure.Presentation.FormatExtensions
{
    /// <summary>
    /// Class UserFormat.
    /// </summary>
    public static class UserFormat
    {
        /// <summary>
        /// Formats the specified user output.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>System.String.</returns>
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
