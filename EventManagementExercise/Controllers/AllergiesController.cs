using EventManagementExercise.Dao;
using EventManagementExercise.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventManagementExercise.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AllergiesController : ControllerBase
  {
    protected readonly IAllergyRepository _allergyRepository;

    public AllergiesController(IAllergyRepository allergyRepository)
    {
      _allergyRepository = allergyRepository;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
      var res = await _allergyRepository.Get();
      return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
      if (id <= 0) return BadRequest();

      var res = await _allergyRepository.GetById(id);
      return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult> Create(AllergyDao allergyDao)
    {
      if (allergyDao.Id > 0) return BadRequest();

      var res = await _allergyRepository.Create(allergyDao);
      return Ok(res);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, AllergyDao allergyDao)
    {
      if (id != allergyDao.Id || allergyDao.Id <= 0) return BadRequest();

      var res = await _allergyRepository.Update(allergyDao);
      return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      if (id <= 0) return BadRequest();
      var res = await _allergyRepository.Delete(id);
      return Ok(res);
    }
  }
}