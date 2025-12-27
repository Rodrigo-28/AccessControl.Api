using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Application.Eceptions;
using AccessControlApi.Application.Interfaces;
using AccessControlApi.Domian.Interfaces;

namespace AccessControlApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IPasswordEncryptionService _passwordEncryptionService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IUserService userService, IPasswordEncryptionService passwordEncryptionService, IJwtTokenService jwtTokenService)
        {
            this._userService = userService;
            this._passwordEncryptionService = passwordEncryptionService;
            this._jwtTokenService = jwtTokenService;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto userDto)
        {
            var user = await _userService.GetOneByEmail(userDto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid email")
                {
                    ErrorCode = "001"
                };
            };
            var isValid = _passwordEncryptionService.VerifyPassword(user.Password, userDto.Password);
            if (!isValid)
            {
                throw new BadRequestException("Invalid password")
                {
                    ErrorCode = "002"
                };
            }


            var token = _jwtTokenService.GenerateJwtToken(user);
            return new LoginResponseDto { Token = token, FirstLogin = user.FirstLogin };

        }
    }
}
