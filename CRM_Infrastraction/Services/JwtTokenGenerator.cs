using CRM_Domain.Entities;
using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static CRM_Interface.Enum.IEnum;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CRM_Infrastraction.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly RoleManager<IdentityRole> _role;
        private readonly UserManager<User> _userManager;
        private readonly JWT _jwt;

        public JwtTokenGenerator(RoleManager<IdentityRole> role, UserManager<User> userManager,IOptions<JWT> jwt)
        {
            _role = role;
            _userManager = userManager;
            _jwt = jwt.Value;
        }
        public async Task<string> GenerateJwtToken(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var userClaims = await _userManager.GetClaimsAsync(user);

            List<Claim> roles = new();
            foreach (var role in userRoles)
            {
                roles.Add(new Claim("role",role));
            }

            string accountType = user.AccountType == (int)AccountType.User ?"User":"Company";

            IEnumerable<Claim> Claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub,user.UserName??""),
                 new Claim(JwtRegisteredClaimNames.Email,user.Email ?? ""),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Name,user.FirstName+" "+user.LastName),
                 new Claim("UID",user.Id),
                 new Claim("AccountType",accountType),
            }
            .Union(roles)
            .Union(userClaims);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            SigningCredentials signingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurity = new(
            
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                expires: DateTime.UtcNow.AddHours(2),
                claims:Claims,
                signingCredentials:signingCredentials

            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurity);

            return token;


        }


        #region Refresh Token
        public RefreshTokenRequestModel GenerateRefreshToken()
        {
            return new RefreshTokenRequestModel
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpireDate = DateTime.UtcNow.AddDays(7),
            };
        }
        #endregion
    }
}
