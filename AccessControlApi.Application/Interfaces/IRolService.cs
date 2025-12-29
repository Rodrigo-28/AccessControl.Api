using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Domian.Models;

namespace AccessControlApi.Application.Interfaces
{
    public interface IRolService : IBaseService<Role, RoleResponseDto, CreateRoleDto, UpdateRoleDto>
    {


    }
}
