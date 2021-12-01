using EventManagementExercise.Dao;
using EventManagementExercise.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementExercise.Interfaces.Repositories
{
  public interface IAllergyRepository
  {
    Task<IList<Allergy>> Get();
    Task<Allergy> GetById(int id);
    Task<bool> Create(AllergyDao allergyDao);
    Task<bool> Update(AllergyDao allergyDao);
    Task<bool> Delete(int id);
  }
}
