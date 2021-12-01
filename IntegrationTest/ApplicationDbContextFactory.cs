using EventManagementExercise.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IntegrationTest
{
  public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {
    public ApplicationDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
      optionsBuilder.UseSqlServer(
        "Server=(localdb)\\mssqllocaldb;Database=aspnet-event-management-testing;Trusted_Connection=True;");

      return new ApplicationDbContext(optionsBuilder.Options);
    }
  }
}
