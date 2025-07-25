using CRM_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CRM_Interface.Enum.IEnum;

namespace CRM_Infrastraction.Persistence
{
    public class SeedData
    {
        public static async Task SeedDataAsync(UserManager<User> userManager,RoleManager<IdentityRole> roleManager,AppDbContext db)
        {
            string[] roles = ["Admin","Manager","User"];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            string admin = "admin@crm.com";
            if (await userManager.FindByEmailAsync(admin) is null)
            {
                User adminAccount = new()
                {
                    FirstName = "admin",
                    LastName = "",
                    FullName = "admin",
                    UserName = admin,
                    Email = admin,
                    PhoneNumber = "010",
                    Address = "",
                    TaxNo = "",
                    AccountType = (int)AccountType.Company,
                    
                };

                await userManager.CreateAsync(adminAccount, "123456Ee*");
                await userManager.AddToRoleAsync(adminAccount, "Admin");


                #region Create Campaigns
                string afaq = "Afaq@crm.com";
                User companyAccount = new()
                {
                    FirstName = " مؤسسة الأفاق الحديثة ",
                    LastName = "",
                    FullName = "Afaq",
                    UserName = afaq,
                    Email = afaq,
                    PhoneNumber = "010",
                    Address = "",
                    TaxNo = "",
                    AccountType = (int)AccountType.Company,

                };

                await userManager.CreateAsync(companyAccount, "123456Ee*");
                await userManager.AddToRoleAsync(companyAccount, "Admin");

                var companyEmail = await userManager.FindByEmailAsync(afaq);
                if (companyEmail is not null)
                {
                    var campaigns = new List<Campaign>
                {
                     new Campaign
                    {
                        Name = "حملة الصيف الكبرى",
                        Description = "ترويج لعروض الصيف على المنتجات الإلكترونية.",
                        Budget = 10000m,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(30),
                        UserId = companyAccount.Id,
                        ImageUrl = "assets/images/summer.png",
                        BudgetAfterDiscount = 8000m, // مثال على الميزانية بعد الخصم
                        RateDiscount = 20m // مثال على نسبة الخصم
                    },
                    new Campaign
                    {
                        Name = "خصومات العودة للمدارس",
                        Description = "عروض خاصة بموسم العودة للمدارس.",
                        Budget = 8000m,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(25),
                        UserId = companyAccount.Id,
                        ImageUrl = "assets/images/school.jpg",
                        BudgetAfterDiscount = 6400m, // مثال على الميزانية بعد الخصم
                        RateDiscount = 20m // مثال على نسبة الخصم
                    },
                    new Campaign
                    {
                        Name = "مهرجان الربيع",
                        Description = "عروض مميزة على الملابس والأحذية بمناسبة حلول الربيع.",
                        Budget = 11000m,
                        StartDate = new DateTime(2025, 03, 15),
                        EndDate = new DateTime(2025, 08, 15),
                        UserId = companyAccount.Id,
                        ImageUrl = "assets/images/spring.jpg",
                        RateDiscount = 15m, // نسبة الخصم
                        BudgetAfterDiscount = 9350m // الميزانية بعد الخصم
                    },
                    new Campaign
                    {
                        Name = "تخفيضات نهاية العام",
                        Description = "أقوى التخفيضات على كافة الأقسام بمناسبة نهاية السنة.",
                        Budget = 15000m,
                        StartDate = new DateTime(2025, 06, 1),
                        EndDate = new DateTime(2025, 12, 31),
                        UserId = companyAccount.Id,
                        ImageUrl = "assets/images/endyear.jpg",
                        RateDiscount = 25m, // نسبة الخصم   
                        BudgetAfterDiscount = 11250m // الميزانية بعد الخصم
                    }
                };

                    await db.Campaigns.AddRangeAsync(campaigns);
                    await db.SaveChangesAsync();
                }

                #endregion
            }

            string userAccount = "user@crm.com";
            if (await userManager.FindByEmailAsync(userAccount) is null)
            {
                User userEmail = new()
                {
                    FirstName = "User",
                    LastName = "",
                    FullName = "user",
                    UserName = "User@test",
                    Email = userAccount,
                    PhoneNumber = "01024*****",
                    Address = "Cairo",
                    TaxNo = "",
                    AccountType = (int)AccountType.User
                };

                await userManager.CreateAsync(userEmail, "123456Ee*");
                await userManager.AddToRoleAsync(userEmail, "User");
            }
        }
    }
}
