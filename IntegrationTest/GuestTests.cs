using EventManagementExercise.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace IntegrationTest
{
  [TestFixture]
  public class GuestTests : IntegrationTestBase
  {
    [Test]
    public async Task CreateGuest()
    {
      Guest guest = new Guest("John", "Doe", "john@doe.com");

      _dbContext.Guests.Add(guest);
      await _dbContext.SaveChangesAsync();

      var res = _dbContext.Guests.Find(guest.Id);
      Assert.AreEqual(res.FirstName, "John");
      Assert.AreEqual(res.LastName, "Doe");
      Assert.AreEqual(res.NormalizedName, "JOHN DOE");
      Assert.AreEqual(res.Email, "john@doe.com");
      Assert.AreEqual(res.NormalizedEmail, "JOHN@DOE.COM");
    }
  }
}
