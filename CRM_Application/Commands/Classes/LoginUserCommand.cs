using CRM_Interface.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Commands.Classes
{
    public class LoginUserCommand:IRequest<ResponseMessage>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
