using Application.Contracts.Requests.Criminal;
using Application.Contracts.Responses;
using Application.Interfaces.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiController, Route("api/[controller]")]
    public class CriminalController : ControllerBase
    {
        private readonly ICriminalService _criminalService;
        public CriminalController(ICriminalService criminalService) {
            this._criminalService = criminalService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CriminalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id) 
            => Ok(await this._criminalService.GetById(id));

        [HttpGet]
        [ProducesResponseType(typeof(List<CriminalResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll() 
            => Ok(await this._criminalService.GetAll());

        [HttpPost]
        [ProducesResponseType(typeof(CriminalResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateCriminalRequest request) {
            CriminalResponse criminalResponse = await this._criminalService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = criminalResponse.Id }, criminalResponse);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CriminalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateCriminalRequest request)
            => Ok(await this._criminalService.Update(request));

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id) {
            await this._criminalService.Delete(id);
            return NoContent();
        }
    }
}
