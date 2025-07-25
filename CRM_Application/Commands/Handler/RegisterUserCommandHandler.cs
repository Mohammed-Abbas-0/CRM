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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseMessage>
    {
        private readonly IAuthLogin _authLogin;
        public RegisterUserCommandHandler(IAuthLogin authLogin)
        {
            _authLogin = authLogin;
        }
        public async Task<ResponseMessage> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            AuthModel authModel = new() {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,    
                Password = request.Password,
                UserName = request.UserName,
                AccountType = request.AccountType,
                Address = request.Address,
                TaxNo = request.TaxNo,
                PhoneNumber = request.PhoneNumber,
            };
            var result = await _authLogin.RegisterAsync(authModel);
            return result;
        }
    }
}
