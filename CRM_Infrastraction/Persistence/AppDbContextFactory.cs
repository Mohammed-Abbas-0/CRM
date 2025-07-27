using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM_Infrastraction.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer("Server=DESKTOP-U2PPPRB\\MSSQLSERVER02;Database=CRMDb;User Id=sa;Password=123456;TrustServerCertificate=True;");
           // optionsBuilder.UseSqlServer("Server=db24164.public.databaseasp.net; Database=db24164; User Id=db24164; Password=2Lq!y4#G+F8k; Encrypt=False; MultipleActiveResultSets=True;");


            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
