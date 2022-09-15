using Store.Domain.Core.Entities;
using Store.Domain.Contracts.DTO;
using System.Linq;

namespace Store.Domain.Contracts.Extensions
{
    /// <summary>
    /// Class UserExtensions.
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Converts to dto.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>RegisteredUserDto.</returns>
        public static RegisteredUserDto ToDto(this RegisteredUser user)
        {
            return new RegisteredUserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType.ToString(),
                CredentialsId = user.Credentials.Id,
                Orders = user.Orders
                    .Select(o => o.ToDto(user.Id)).ToList()
            };
        }
    }
}
