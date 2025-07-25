using CRM_Domain.Entities;
using CRM_Infrastraction.Persistence;
using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CRM_Interface.Enum.IEnum;

namespace CRM_Infrastraction.Services
{
    public class AuthLogin : IAuthLogin
    {
        private readonly AppDbContext _db;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<User> _userManager;
        public AuthLogin(AppDbContext db, UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ResponseMessage> LoginAsync(LoginModelDto loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user is null)
                return new ResponseMessage { Message="البريد او الباسورد خطأ", IsAuthenticated=false};

            bool isPasswordExisted = await _userManager.CheckPasswordAsync(user,loginModel.Password);
            if (!isPasswordExisted)
                return new ResponseMessage { Message = "البريد او الباسورد خطأ", IsAuthenticated = false };

            string token = await _jwtTokenGenerator.GenerateJwtToken(user);

            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenExpiryTime = refreshToken.ExpireDate;

            await _userManager.UpdateAsync(user);

            return new TokenResponseModel
            {
                IsAuthenticated = true,
                Message = "تم تسجيل الدخول",
                Token = token,
                RefreshToken = refreshToken.Token,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? "",
                UserId = user.Id,
                UserName = user.UserName ?? ""
            };
        }

        public async Task<ResponseMessage> RegisterAsync(AuthModel authModel)
        {
            bool userNameIsExisted = await _userManager.Users.AnyAsync(idx => idx.UserName == authModel.UserName);

            if (userNameIsExisted) 
                return new ResponseMessage { Message = "بالفعل اسم المستخدم مستخدم من قبل" , IsAuthenticated = false};

            bool emailIsExisted = await _userManager.Users.AnyAsync(idx => idx.Email == authModel.Email);

            if (emailIsExisted)
                return new ResponseMessage { Message = "بالفعل البريد مستخدم من قبل", IsAuthenticated = false };

            if(authModel.AccountType == AccountType.Company && string.IsNullOrWhiteSpace(authModel.TaxNo))
            {
                return new ResponseMessage { Message = " يجب وضع رقم ضريبي للشركة", IsAuthenticated = false };

            }

            User user = new User
            { 
                FirstName = authModel.FirstName,
                LastName = authModel.LastName, 
                FullName = authModel.FirstName + " " + authModel.LastName,
                Email = authModel.Email,
                UserName = authModel.UserName,
                PhoneNumber = authModel.PhoneNumber,
                Address = authModel.Address,
                AccountType = (int)authModel.AccountType,
                TaxNo = authModel.TaxNo,
                
            };
            IdentityResult result = await _userManager.CreateAsync(user,authModel.Password);

            var errorResponseMessage = ErrorResponseMessage(result,result.Succeeded);
            if (!errorResponseMessage.IsAuthenticated)
                return errorResponseMessage;

            return new ResponseMessage { Message = $"تم إنشاء المستخدم: {user.UserName} بنجاح", IsAuthenticated = true };
        }


        public async Task<ResponseMessage> AddRoleAsync(AddRoleDto addRoleDto)
        {
            var user = await _userManager.FindByIdAsync(addRoleDto.UserId);
            if(user is null)
                return new ResponseMessage { Message = "المستخدم غير موحود", IsAuthenticated = false };
            bool roleExisted = await _userManager.IsInRoleAsync(user, addRoleDto.RoleName);

            if (roleExisted)
                return new ResponseMessage { Message = "بالفعل هذه الصلاحية مسجلة", IsAuthenticated = false };


            IdentityResult result = await _userManager.AddToRoleAsync(user, addRoleDto.RoleName);

            var errorResponseMessage = ErrorResponseMessage(result, result.Succeeded);
            if (!errorResponseMessage.IsAuthenticated)
                return errorResponseMessage;

            return new ResponseMessage { Message = "تم إضاقة الصلاحيات بنجاح", IsAuthenticated = true };
        }


        private ResponseMessage ErrorResponseMessage(IdentityResult result,bool isSucceeded)
        {
            if (!isSucceeded)
            {
                StringBuilder errorMsg = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errorMsg.Append(error.Description + " | ");
                }
                return new ResponseMessage { Message = errorMsg.ToString(), IsAuthenticated = false };
            }
            return  new ResponseMessage { Message = "", IsAuthenticated = true };
        }

    }
}
