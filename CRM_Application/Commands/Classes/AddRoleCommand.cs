using CRM_Interface.Dtos;
using MediatR;

namespace CRM_Application.Commands.Classes
{
    public class AddRoleCommand:IRequest<ResponseMessage>
    {
        public required string UserId { get; set; }
        public required string RoleName { get; set; }
    }
}
