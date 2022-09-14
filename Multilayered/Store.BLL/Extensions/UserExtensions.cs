using Store.BLL.DTO;
using Store.DAL.Users;
using System.Linq;

namespace Store.BLL.Extensions
{
    public static class UserExtensions
    {
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
