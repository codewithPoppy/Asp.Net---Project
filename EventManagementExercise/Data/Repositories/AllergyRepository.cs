using EventManagementExercise.Dao;
using EventManagementExercise.Interfaces.Repositories;
using EventManagementExercise.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementExercise.Data.Repositories
{
  public class AllergyRepository : IAllergyRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public AllergyRepository(ApplicationDbContext context)
    {
      _dbContext = context;
    }

    public async Task<IList<Allergy>> Get()
    {
      var allergies = await _dbContext.Allergies.AsNoTracking().ToListAsync();
      return allergies;
    }

    public async Task<Allergy> GetById(int id)
    {
      var allergy = await _dbContext.Allergies.AsQueryable().AsNoTracking()
        .Where(x => x.Id == id).FirstOrDefaultAsync();
      return allergy;
    }

    public async Task<bool> Create(AllergyDao allergyDao)
    {
      Allergy allergy = new Allergy(allergyDao.Name);
      await _dbContext.Allergies.AddAsync(allergy);
      await _dbContext.SaveChangesAsync();
      return true;
    }

    public async Task<bool> Update(AllergyDao allergyDao)
    {
      var allergy = await _dbContext.Allergies.Where(x => x.Id == allergyDao.Id).FirstOrDefaultAsync();
      if (allergy == null) return false;

      allergy.FromDao(allergyDao);
      await _dbContext.SaveChangesAsync();
      return true;
    }

    public async Task<bool> Delete(int id)
    {
      var allergy = await _dbContext.Allergies.Where(x => x.Id == id).FirstOrDefaultAsync();
      if (allergy == null) return false;

      _dbContext.Allergies.Remove(allergy);
      await _dbContext.SaveChangesAsync();
      return true;
    }
  }
}