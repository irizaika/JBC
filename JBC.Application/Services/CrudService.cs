using JBC.Application.Helpers;
using JBC.Application.Interfaces;

namespace JBC.Application.Services
{
    public abstract class CrudService<TDto, TEntity> : ICrudService<TDto>
        where TEntity : class
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IGenericRepository<TEntity> _repo;
        protected readonly IMapper<TEntity, TDto> _mapper;

        protected CrudService(IUnitOfWork uow, IMapper<TEntity, TDto> mapper, IGenericRepository<TEntity> repo)
        {
            _uow = uow;
            _mapper = mapper;
            _repo = repo;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(e => _mapper.ToDto(e));
        }

        public virtual async Task<TDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.ToDto(entity);
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.ToEntity(dto);
            await _repo.AddAsync(entity);
            await _uow.SaveAsync();
            return _mapper.ToDto(entity);
        }

        public virtual async Task UpdateAsync(int id, TDto dto)
        {
            var entity = _mapper.ToEntity(dto);
            _repo.Update(entity);
            await _uow.SaveAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
            {
                _repo.Remove(entity);
                await _uow.SaveAsync();
            }
        }
    }
}
