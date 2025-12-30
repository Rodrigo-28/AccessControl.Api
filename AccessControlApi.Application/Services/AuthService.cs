using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Application.Eceptions;
using AccessControlApi.Application.Interfaces;
using AccessControlApi.Domian.Enums;
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

        public async Task<GenericResponseDto> ChangePassword(ChangePasswordRequestDto changePasswordRequestDto, int userId)
        {
            var isPasswordValid = await _userService.VerifyPassword(userId, changePasswordRequestDto.CurrentPassword);
            if (!isPasswordValid)
            {
                throw new BadRequestException("Invalid password")
                {
                    ErrorCode = "001"
                };
            }

            await _userService.ChangePassword(userId, changePasswordRequestDto.NewPassword);


            return new GenericResponseDto { Success = true };
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

        public async Task<GenericResponseDto> Register(RegisterDto registerDto)
        {
            CreateUserDto createUserDto = new CreateUserDto();
            createUserDto.Username = registerDto.Username;
            createUserDto.Email = registerDto.Email;
            createUserDto.Password = registerDto.Password;

            createUserDto.RoleId = (int)UserRole.User;
            await _userService.Create(createUserDto);
            return new GenericResponseDto { Success = true };
        }
    }
}
