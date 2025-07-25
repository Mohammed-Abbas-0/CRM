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
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResponseMessage>
    {
        private readonly IAuthLogin _authLogin;
        public LoginUserCommandHandler(IAuthLogin authLogin)
        {
            _authLogin = authLogin;
        }
        public async Task<ResponseMessage> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            LoginModelDto loginModelDto = new()
            {
                Email = request.Email,
                Password = request.Password,
            };
            var loginRequest = await _authLogin.LoginAsync(loginModelDto);
            return loginRequest;

        }
    }
}
