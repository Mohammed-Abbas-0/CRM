using CRM_Interface.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Interfaces
{
    public interface IAuthLogin
    {
        Task<ResponseMessage> LoginAsync(LoginModelDto loginModel);
        Task<ResponseMessage> RegisterAsync(AuthModel authModel);
        Task<ResponseMessage> AddRoleAsync(AddRoleDto addRoleDto);
    }
}
