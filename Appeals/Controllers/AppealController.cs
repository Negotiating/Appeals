using Appeals.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Appeals.Models;

namespace Appeals.Controllers
{
    [Route("[Appeal]")]
    public class AppealController : Controller
    {
       private readonly IAppealService _appealService;

        public AppealController(IAppealService appealService)
        {
            _appealService = appealService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appeal>>> Appeals()
        {
            return Ok(await _appealService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appeal>> Appeal(int id)
        {
            return Ok(await _appealService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Appeal>> Create(Appeal appeal)
        {
            await _appealService.AddAsync(appeal);
            return CreatedAtAction(nameof(Appeal), new { id = appeal.Id }, appeal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Appeal appeal)
        {
            if (id != appeal.Id)
            {
                return BadRequest();
            }
            await _appealService.UpdateAsync(appeal);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _appealService.DeleteAsync(id);
            return NoContent();
        }
    }
}