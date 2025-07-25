using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public class TokenResponseModel: ResponseMessage
    {
        public required string Token { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string RefreshToken { get; set; }
    }
}
