using Application.Contracts.Requests.Crime;
using Application.Contracts.Responses;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController, Route("api/[controller]")]

    public class CrimeController : Controller
    {
        private readonly ICrimeService _crimeService;
        public CrimeController(ICrimeService crimeService) {
            this._crimeService = crimeService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CrimeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await this._crimeService.GetById(id));

        [HttpGet]
        [ProducesResponseType(typeof(List<CrimeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Index()
            => Ok(await this._crimeService.GetAll());

        [HttpPost]
        [ProducesResponseType(typeof(CrimeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateCrimeRequest request) {
            CrimeResponse crimeResponse = await this._crimeService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = crimeResponse.Id }, crimeResponse);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CrimeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateCrimeRequest request)
            => Ok(await this._crimeService.Update(request));


        [HttpPut("Solved")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrimeSolved(int id) {
            await this._crimeService.Solved(id);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id) {
            await this._crimeService.Delete(id);
            return NoContent();
        }
    }
}