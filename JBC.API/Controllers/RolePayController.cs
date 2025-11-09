using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolePayController : ControllerBase
    {
        private readonly IRolePayService _rolePayService;

        public RolePayController(IRolePayService rolePayService)
        {
            _rolePayService = rolePayService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolePayRatePerJobCategoryDto>>> GetAll()
        {
            var rolePays = await _rolePayService.GetAllAsync();
            return Ok(rolePays);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolePayRatePerJobCategoryDto>> Get(int id)
        {
            var rolePay = await _rolePayService.GetByIdAsync(id);
            if (rolePay == null) return NotFound();
            return Ok(rolePay);
        }

        [HttpPost]
        public async Task<ActionResult<RolePayRatePerJobCategoryDto>> Create(RolePayRatePerJobCategoryDto rolePayDto)
        {
            var result = await _rolePayService.CreateAsync(rolePayDto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RolePayRatePerJobCategoryDto rolePayDto)
        {
            await _rolePayService.UpdateAsync(id, rolePayDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _rolePayService.DeleteAsync(id);
            return NoContent();
        }
    }
}
