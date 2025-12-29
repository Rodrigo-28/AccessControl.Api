using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Domian.Models;

namespace AccessControlApi.Application.Interfaces
{
    public interface IUserService : IBaseService<User, UserResponseDto, CreateUserDto, UpdateUserDto>
    {
        Task<User> GetOneByEmail(string email);
        Task<bool> VerifyPassword(int userId, string password);
        Task<GenericResponseDto> ChangePassword(int userId, string password);
    }
}
