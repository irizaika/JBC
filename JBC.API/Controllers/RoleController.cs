using AutoMapper;
using JBC.Application.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RoleController(IUnitOfWork uow, IMapper mapper) 
        { 
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            var roles = await _uow.Roles.GetAllAsync();
            var roleDtos  = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Ok(roleDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> Get(int id)
        {
            var role = await _uow.Roles.GetByIdAsync(id);
            if (role == null) return NotFound();

            var roleDto = _mapper.Map<RoleDto>(role);
            return Ok(roleDto);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);

            await _uow.Roles.AddAsync(role);
            await _uow.SaveAsync();

            var responceDto = _mapper.Map<RoleDto>(role);


            return CreatedAtAction(nameof(Get), new { id = roleDto.Id }, responceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoleDto roleDto)
        {
            if (id != roleDto.Id) return BadRequest();

            var role = _mapper.Map<Role>(roleDto);

            _uow.Roles.Update(role);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _uow.Roles.GetByIdAsync(id);
            if (role == null) return NotFound();
            _uow.Roles.Remove(role);
            await _uow.SaveAsync();
            return NoContent();
        }
    }
}
