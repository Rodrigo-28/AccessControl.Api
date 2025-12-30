using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RoleController(IRolService rolService)
        {
            this._rolService = rolService;
        }
        [Authorize(Policy = "Admin")]
        [HttpGet("{rolId}")]
        public async Task<IActionResult> GetOne(int rolId)
        {
            var role = await _rolService.GetOne(rolId);
            return Ok(role);
        }
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _rolService.GetAll();
            return Ok(res);
        }
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleDto createRoleDto)
        {
            var role = await _rolService.Create(createRoleDto);
            return Ok(role);
        }
        [Authorize(Policy = "Admin")]
        [HttpDelete("{roleId}/remove")]
        public async Task<IActionResult> RemoveRol(int roleId)
        {
            var role = await _rolService.Delete(roleId);
            return Ok(role);

        }
        [Authorize(Policy = "Admin")]
        [HttpPut("{roleId}")]
        public async Task<IActionResult> Update(int roleId, [FromBody] UpdateRoleDto updateRoleDto)
        {
            var role = await _rolService.Update(roleId, updateRoleDto);
            return Ok(role);
        }
    }
}
