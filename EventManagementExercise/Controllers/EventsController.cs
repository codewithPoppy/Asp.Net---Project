using EventManagementExercise.Dao;
using EventManagementExercise.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventManagementExercise.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventsController : ControllerBase
  {
    protected readonly IEventRepository _eventRepository;

    public EventsController(IEventRepository eventRepository)
    {
      _eventRepository = eventRepository;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
      var res = await _eventRepository.Get();
      return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
      if (id <= 0) return BadRequest();

      var res = await _eventRepository.GetById(id);
      return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult> Create(EventDao eventDao)
    {
      if (eventDao.Id > 0 || eventDao.Guests.Count < 2) return BadRequest();

      var res = await _eventRepository.Create(eventDao);
      return Ok(res);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, EventDao eventDao)
    {
      if (id != eventDao.Id || eventDao.Id <= 0 || eventDao.Guests.Count < 2) return BadRequest();

      var res = await _eventRepository.Update(eventDao);
      return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      if (id <= 0) return BadRequest();
      var res = await _eventRepository.Delete(id);
      return Ok(res);
    }
  }
}
