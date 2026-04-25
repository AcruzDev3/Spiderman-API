using Application.Contracts.Requests.CrimeType;
using Application.Contracts.Responses;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiController, Route("api/[controller]")]
    public class CrimeTypeController : ControllerBase
    {
        private readonly ICrimeTypeService _crimeTypeService;
        public CrimeTypeController(ICrimeTypeService crimeTypeService) {
            this._crimeTypeService = crimeTypeService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CrimeTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await this._crimeTypeService.GetById(id));

        [HttpGet]
        [ProducesResponseType(typeof(List<CrimeTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
            => Ok(await this._crimeTypeService.GetAll());

        [HttpPost]
        [ProducesResponseType(typeof(CrimeTypeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateCrimeTypeRequest request) {
            CrimeTypeResponse crimeTypeResponse = await this._crimeTypeService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = crimeTypeResponse.Id }, crimeTypeResponse);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CrimeTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateCrimeTypeRequest request)
            => Ok(await this._crimeTypeService.Update(request));

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id) {
            await this._crimeTypeService.Delete(id);
            return NoContent();
        }
    }
}
