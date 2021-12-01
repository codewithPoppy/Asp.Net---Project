using NUnit.Framework;
using System.Threading.Tasks;

namespace IntegrationTest
{
  [SetUpFixture]
  public class IntegrationSetup
  {
    [OneTimeSetUp]
    public async Task SetUpDatabase()
    {
      var dbContextFactory = new ApplicationDbContextFactory();
      using (var dbContext = dbContextFactory.CreateDbContext(new string[] { }))
      {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
      }
    }
  }
}
