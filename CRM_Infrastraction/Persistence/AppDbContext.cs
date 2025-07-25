using CRM_Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRM_Infrastraction.Persistence
{
    public  class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<CampaignInteraction> CampaignInteractions => Set<CampaignInteraction>();
        public DbSet<Interaction> Interactions => Set<Interaction>();
        public DbSet<CampaignRegistration> CampaignRegistrations => Set<CampaignRegistration>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            

        }


    }
}
