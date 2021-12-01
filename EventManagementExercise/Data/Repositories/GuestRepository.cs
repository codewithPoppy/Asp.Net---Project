using EventManagementExercise.Dao;
using EventManagementExercise.Interfaces.Repositories;
using EventManagementExercise.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementExercise.Data.Repositories
{
  public class GuestRepository : IGuestRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public GuestRepository(ApplicationDbContext context)
    {
      _dbContext = context;
    }

    public async Task<IList<Guest>> Get()
    {
      var guests = await _dbContext.Guests.AsNoTracking()
        .Include(x => x.Allergies).ToListAsync();
      return guests;
    }

    public async Task<Guest> GetById(int id)
    {
      var guest = await _dbContext.Guests.AsQueryable().AsNoTracking()
        .Where(x => x.Id == id)
        .Include(x => x.Allergies)
        .FirstOrDefaultAsync();
      return guest;
    }

    public async Task<bool> Create(GuestDao guestDao)
    {
      Guest guest = new Guest(guestDao.FirstName, guestDao.LastName, guestDao.Email);
      await _dbContext.Guests.AddAsync(guest);

      await AddAllergies(guest, guestDao.Allergies);
      await _dbContext.SaveChangesAsync();
      return true;
    }

    public async Task<bool> Update(GuestDao guestDao)
    {
      var guest = await _dbContext.Guests.Where(x => x.Id == guestDao.Id)
        .Include(x => x.Allergies).FirstOrDefaultAsync();
      if (guest == null) return false;

      guest.FromDao(guestDao);

      await AddAllergies(guest, guestDao.Allergies);
      await _dbContext.SaveChangesAsync();
      return true;
    }

    public async Task<bool> Delete(int id)
    {
      var guest = await _dbContext.Guests.Where(x => x.Id == id).FirstOrDefaultAsync();
      if (guest == null) return false;

      _dbContext.Guests.Remove(guest);
      await _dbContext.SaveChangesAsync();
      return true;
    }

    private async Task<bool> AddAllergies(Guest guest, IList<AllergyDao> allergyDaos)
    {
      int index = 0;
      while (index < guest.Allergies.Count)
      {
        var allergy = guest.Allergies.ElementAt(index);
        var allergyDao = allergyDaos.Where(x => x.Name.ToUpper() == allergy.NormalizedName).FirstOrDefault();
        if (allergyDao == null)
          guest.Allergies.Remove(allergy);
        else
        {
          allergyDaos.Remove(allergyDao);
          index++;
        }
      }
      foreach (AllergyDao allergyDao in allergyDaos)
      {
        Allergy allergy = null;
        if (allergyDao?.Id > 0)
          allergy = await _dbContext.Allergies.AsNoTracking()
            .Where(x => x.Id == allergyDao.Id).FirstOrDefaultAsync();
        else
        {
          allergy = await _dbContext.Allergies.AsNoTracking()
            .Where(x => x.NormalizedName == allergyDao.Name).FirstOrDefaultAsync();
          if (allergy == null)
            allergy = new Allergy(allergyDao.Name);
        }
        guest.Allergies.Add(allergy);
      }
      return true;
    }
  }
}
