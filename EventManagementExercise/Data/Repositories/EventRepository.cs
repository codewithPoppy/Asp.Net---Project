using EventManagementExercise.Dao;
using EventManagementExercise.Interfaces.Repositories;
using EventManagementExercise.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementExercise.Data.Repositories
{
  public class EventRepository : IEventRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public EventRepository(ApplicationDbContext context)
    {
      _dbContext = context;
    }

    public async Task<IList<Event>> Get()
    {
      var events = await _dbContext.Events.AsNoTracking()
        .Include(x => x.Guests).ToListAsync();
      return events;
    }

    public async Task<Event> GetById(int id)
    {
      var eventObj = await _dbContext.Events.AsQueryable().AsNoTracking()
        .Include(x => x.Guests)
        .Where(x => x.Id == id).FirstOrDefaultAsync();
      return eventObj;
    }

    public async Task<bool> Create(EventDao eventDao)
    {
      Event newEvent = new Event { Name = eventDao.Name, Date = eventDao.Date };
      await _dbContext.Events.AddAsync(newEvent);

      await AddGuests(newEvent, eventDao.Guests);
      await _dbContext.SaveChangesAsync();
      return true;
    }

    public async Task<bool> Update(EventDao eventDao)
    {
      var eventObj = await _dbContext.Events.Where(x => x.Id == eventDao.Id)
        .Include(x => x.Guests).FirstOrDefaultAsync();
      if (eventObj == null) return false;

      eventObj.Name = eventDao.Name;
      eventObj.Date = eventDao.Date;

      await AddGuests(eventObj, eventDao.Guests);
      await _dbContext.SaveChangesAsync();
      return true;
    }

    public async Task<bool> Delete(int id)
    {
      var eventObj = await _dbContext.Events.Where(x => x.Id == id).FirstOrDefaultAsync();
      if (eventObj == null) return false;

      _dbContext.Events.Remove(eventObj);
      await _dbContext.SaveChangesAsync();
      return true;
    }

    private async Task<bool> AddGuests(Event ev, IList<int> guests)
    {
      int index = 0;
      while (index < ev.Guests.Count)
      {
        var guest = ev.Guests.ElementAt(index);
        var guestId = guests.Where(x => x == guest.Id).FirstOrDefault();
        if (guestId == 0)
          ev.Guests.Remove(guest);
        else
        {
          guests.Remove(guestId);
          index++;
        }
      }
      foreach (int guestId in guests)
      {
        if (ev.Guests.Where(x => x.Id == guestId).Count() > 0) continue;
        Guest guest = await _dbContext.Guests.AsNoTracking().Where(x => x.Id == guestId).FirstOrDefaultAsync();
        ev.Guests.Add(guest);
      }
      return true;
    }
  }
}