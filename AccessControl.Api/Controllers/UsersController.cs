using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Eceptions;
using AccessControlApi.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }
        [Authorize(Policy = "Admin")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOne(int userId)
        {
            var user = await _userService.GetOne(userId);
            return Ok(user);
        }
        //[Authorize(Policy = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto, IValidator<CreateUserDto> validator)
        {
            var validationResult = await validator.ValidateAsync(createUserDto);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString())
                {
                    ErrorCode = "004"
                };
            }

            var user = await _userService.Create(createUserDto);
            return Ok(user);
        }
        [Authorize(Policy = "Admin")]

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }
        [Authorize(Policy = "Admin")]

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(int userId, [FromBody] UpdateUserDto updateUserDto, IValidator<UpdateUserDto> validator)
        {
            var validationResult = await validator.ValidateAsync(updateUserDto);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString());
            }
            var user = await _userService.Update(userId, updateUserDto);
            return Ok(user);
        }
        [Authorize(Policy = "Admin")]

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var userDeleted = await _userService.Delete(userId);
            return Ok(userDeleted);
        }
    }
}
