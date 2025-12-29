using AccessControlApi.Application.Dtos.Responses;

namespace AccessControlApi.Application.Interfaces
{
    public interface IBaseService<TEntity, TResponseDto, TCreateDto, TUpdateDto>
    {
        Task<IEnumerable<TResponseDto>> GetAll();
        Task<TResponseDto> Create(TCreateDto body);
        Task<TResponseDto> Update(int id, TUpdateDto body);
        Task<TResponseDto?> GetOne(int id);
        Task<GenericResponseDto> Delete(int id);
    }
}
