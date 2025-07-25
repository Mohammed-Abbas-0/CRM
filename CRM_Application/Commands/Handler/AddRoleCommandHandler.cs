using CRM_Application.Commands.Classes;
using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Application.Commands.Handler
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, ResponseMessage>
    {
        private readonly IAuthLogin _authLogin;
        public AddRoleCommandHandler(IAuthLogin authLogin)
        {
            _authLogin = authLogin;
        }
        public async Task<ResponseMessage> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            AddRoleDto addRoleDto = new() { RoleName = request.RoleName ,UserId = request.UserId};

            var addRoleRequest = await _authLogin.AddRoleAsync(addRoleDto);

            return addRoleRequest;
        }
    }
}
