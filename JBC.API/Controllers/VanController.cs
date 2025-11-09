using JBC.Application.Intefraces.CrudInterfaces;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VanController : ControllerBase
    {
        private readonly IVanService _vanService;

        public VanController(IVanService vanService)
        {
            _vanService = vanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VanDto>>> GetAll()
        {
            var vans = await _vanService.GetAllAsync();
            return Ok(vans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VanDto>> Get(int id)
        {
            var van = await _vanService.GetByIdAsync(id);
            if (van == null) return NotFound();
            return Ok(van);
        }

        [HttpPost]
        public async Task<ActionResult<VanDto>> Create(VanDto vanDto)
        {
            var result = await _vanService.CreateAsync(vanDto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VanDto vanDto)
        {
            await _vanService.UpdateAsync(id, vanDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vanService.DeleteAsync(id);
            return NoContent();
        }
    }
}
