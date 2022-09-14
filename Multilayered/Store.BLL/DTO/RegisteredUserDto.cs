using System.Collections.Generic;

namespace Store.BLL.DTO
{
    public class RegisteredUserDto: BaseEntityDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
        public string CredentialsId { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}
