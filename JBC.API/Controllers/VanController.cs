using AutoMapper;
using JBC.Application.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VanController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VanController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VanDto>>> GetAll()
        {
            try
            {
                var vans = await _uow.Vans.GetAllAsync();
                var vanDto = _mapper.Map<IEnumerable<VanDto>>(vans);
                return Ok(vanDto);
            }
            catch  (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VanDto>> Get(int id)
        {
            var van = await _uow.Vans.GetByIdAsync(id);
            if (van == null) return NotFound();
            var vanDto = _mapper.Map<VanDto>(van);
            return Ok(vanDto);
        }

        [HttpPost]
        public async Task<ActionResult<VanDto>> Create(VanDto vanDto)
        {
            var van = _mapper.Map<Van>(vanDto);

            await _uow.Vans.AddAsync(van);
            await _uow.SaveAsync();

            var responceDto = _mapper.Map<VanDto>(van);

            return CreatedAtAction(nameof(Get), new { id = van.Id }, responceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VanDto vanDto)
        {
            await Task.Delay(2000);
           // return BadRequest();
            if (id != vanDto.Id) return BadRequest();

            var van = _mapper.Map<Van>(vanDto);

            _uow.Vans.Update(van);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var van = await _uow.Vans.GetByIdAsync(id);

            if (van == null) return NotFound();

            _uow.Vans.Remove(van);
            await _uow.SaveAsync();

            return NoContent();
        }
    }
}
