using Application.Contracts.Requests.Role;
using Application.Contracts.Responses;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService) {
            this._roleService = roleService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await this._roleService.GetById(id));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await this._roleService.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequest request) {
            RoleResponse roleResponse = await this._roleService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = roleResponse.Id }, roleResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleRequest request)
            => Ok(await this._roleService.Update(request));

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            await this._roleService.Delete(id);
            return NoContent();
        }
    }
}
