using EventManagementExercise.Dao;
using EventManagementExercise.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementExercise.Interfaces.Repositories
{
  public interface IEventRepository
  {
    Task<IList<Event>> Get();
    Task<Event> GetById(int id);
    Task<bool> Create(EventDao eventDao);
    Task<bool> Update(EventDao eventDao);
    Task<bool> Delete(int id);
  }
}
