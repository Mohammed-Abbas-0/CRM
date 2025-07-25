using CRM_Domain.Entities;
using CRM_Infrastraction.Persistence;
using CRM_Infrastraction.Repositories;
using CRM_Infrastraction.Services;
using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using CRM_Interface.IRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM_Infrastraction.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection Services, IConfiguration Configuration)
        {

            Services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            Services.AddDbContext<AppDbContext>(idx => idx.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Services.AddScoped<ICampaignRepository, CampaignRepository>();
            Services.AddScoped<IClientRepository, ClientRepository>();
            Services.AddScoped<IAuthLogin, AuthLogin>();
            Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            Services.AddScoped<IDashboard, Dashboard>();


            Services.Configure<JWT>(Configuration.GetSection("JWT"));

            return Services;

        }

        public static async Task SeedIdentityDataAsync(this IApplicationBuilder app)
        {
            /*
             🔹 شرح:
                هنا بننشئ scope جديد للخدمات 
            (Dependency Injection Scope)
            باستخدام CreateScope().

                🔹 ليه؟
                لأن بعض الخدمات (زي UserManager و RoleManager) بيشتغلوا ضمن Scoped Lifetime، فلازم تشتغل عليهم داخل
            scope مؤقت، علشان يتم التخلص منهم بعد الاستخدام.


             */
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var dbContext = services.GetRequiredService<AppDbContext>();

            await SeedData.SeedDataAsync(userManager, roleManager,dbContext);
        }
    }
}
