using CRM_Application.Commands.Handler;
using CRM_Application.Queries.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM_Application.MediatRDependencies
{
    public static class MediatRDependency
    {
        public static IServiceCollection AddMediatRDependency(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddMediatR(idx => idx.RegisterServicesFromAssembly(typeof(CreateCampaignCommandHandler).Assembly));
            Services.AddMediatR(idx => idx.RegisterServicesFromAssembly(typeof(UpdateCampaignCommandHandler).Assembly));
            Services.AddMediatR(idx => idx.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly));
            Services.AddMediatR(idx => idx.RegisterServicesFromAssembly(typeof(LoginUserCommandHandler).Assembly));
            Services.AddMediatR(idx => idx.RegisterServicesFromAssembly(typeof(AddRoleCommandHandler).Assembly));
            Services.AddMediatR(idx => idx.RegisterServicesFromAssembly(typeof(RegisterCampaignCommandHandler).Assembly));
            Services.AddMediatR(idx => idx.RegisterServicesFromAssembly(typeof(UnRegisterCampaignCommandHandler).Assembly));
            Services.AddMediatR(idx => idx.RegisterServicesFromAssembly(typeof(DeleteCampaignCommandHandler).Assembly));
            
            
            return Services;

        }
    }
}
