using EventManagementExercise.Dao;
using EventManagementExercise.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventManagementExercise.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GuestsController : ControllerBase
  {
    protected readonly IGuestRepository _guestRepository;

    public GuestsController(IGuestRepository guestRepository)
    {
      _guestRepository = guestRepository;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
      var res = await _guestRepository.Get();
      return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
      if (id <= 0) return BadRequest();

      var res = await _guestRepository.GetById(id);
      return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult> Create(GuestDao guestDao)
    {
      if (guestDao.Id > 0) return BadRequest();

      var res = await _guestRepository.Create(guestDao);
      return Ok(res);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, GuestDao guestDao)
    {
      if (id != guestDao.Id || guestDao.Id <= 0) return BadRequest();

      var res = await _guestRepository.Update(guestDao);
      return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      if (id <= 0) return BadRequest();
      var res = await _guestRepository.Delete(id);
      return Ok(res);
    }
  }
}