using Application.Contracts.Requests.CriminalRiskLevel;
using Application.Contracts.Requests.Role;
using Application.Contracts.Responses;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CriminalRiskLevelController : Controller
    {
        private readonly ICriminalRiskLevelService _criminalRiskLevelService;
        public CriminalRiskLevelController(ICriminalRiskLevelService roleService) {
            this._criminalRiskLevelService = roleService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CriminalRiskLevelResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await this._criminalRiskLevelService.GetById(id));

        [HttpGet]
        [ProducesResponseType(typeof(List<CriminalRiskLevelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
            => Ok(await this._criminalRiskLevelService.GetAll());

        [HttpPost]
        [ProducesResponseType(typeof(CriminalRiskLevelResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateCriminalRiskLevelRequest request) {
            CriminalRiskLevelResponse criminalRiskLevelResponse = await this._criminalRiskLevelService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = criminalRiskLevelResponse.Id }, criminalRiskLevelResponse);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CriminalRiskLevelResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateCriminalRiskLevelRequest request)
            => Ok(await this._criminalRiskLevelService.Update(request));

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id) {
            await this._criminalRiskLevelService.Delete(id);
            return NoContent();
        }
    }
}
