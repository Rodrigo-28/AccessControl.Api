using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;

namespace AccessControlApi.Application.Interfaces
{
    public interface IRolService
    {
        Task<RoleResponseDto?> GetOne(int rolId);
        Task<IEnumerable<RoleResponseDto>> GetAll();

        Task<RoleResponseDto?> Create(CreateRoleDto createRoleDto);
        Task<RoleResponseDto> Update(int roleId, UpdateRoleDto updateRoleDto);
        Task<GenericResponseDto> Delete(int roleId);

    }
}
