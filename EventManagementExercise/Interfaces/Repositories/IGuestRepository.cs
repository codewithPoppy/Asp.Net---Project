using EventManagementExercise.Dao;
using EventManagementExercise.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementExercise.Interfaces.Repositories
{
  public interface IGuestRepository
  {
    Task<IList<Guest>> Get();
    Task<Guest> GetById(int id);
    Task<bool> Create(GuestDao guestDao);
    Task<bool> Update(GuestDao guestDao);
    Task<bool> Delete(int id);
  }
}
