using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Eceptions;
using AccessControlApi.Application.Interfaces;
using AccessControlApi.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto userDto, IValidator<LoginRequestDto> validator)
        {
            var validationResult = await validator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString())
                {
                    ErrorCode = "004"
                };
            }

            var token = await _authService.Login(userDto);
            return Ok(token);
        }

        [Authorize(Policy = "User")]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequestDto, IValidator<ChangePasswordRequestDto> validator)
        {
            var validationResult = await validator.ValidateAsync(changePasswordRequestDto);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString())
                {
                    ErrorCode = "004"
                };
            }
            var userId = UserHelper.GetRequiredUserId(User);
            var response = await _authService.ChangePassword(changePasswordRequestDto, userId);
            return Ok(response);
        }
    }
}
