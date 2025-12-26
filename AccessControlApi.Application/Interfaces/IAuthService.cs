using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;

namespace AccessControlApi.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto userDto);
    }
}
