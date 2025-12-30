using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Domian.Common;
using AccessControlApi.Domian.Models;

namespace AccessControlApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAll();
        Task<UserResponseDto> GetOne(int userId);
        Task<UserResponseDto> Create(CreateUserDto createUserDto);

        Task<UserResponseDto> Update(int userId, UpdateUserDto updateUserDto);
        Task<GenericResponseDto> Delete(int userId);
        Task<User> GetOneByEmail(string email);

        Task<bool> VerifyPassword(int userId, string password);
        Task<GenericResponseDto> ChangePassword(int userId, string password);
        Task<GenericListResponse<UserResponseDto>> GetList(int page, int pageSize, int? roleId, string? email);
    }
}
