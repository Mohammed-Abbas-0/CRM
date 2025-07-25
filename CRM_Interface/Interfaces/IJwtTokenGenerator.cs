using CRM_Domain.Entities;
using CRM_Interface.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateJwtToken(User user);
        RefreshTokenRequestModel GenerateRefreshToken();
    }
}
