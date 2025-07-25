using CRM_Interface.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CRM_Interface.Enum.IEnum;

namespace CRM_Application.Commands.Classes
{
    public class RegisterUserCommand:IRequest<ResponseMessage>
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public required AccountType AccountType { get; set; } // 1. User   2. Company
        public string? TaxNo { get; set; }
    }
}
