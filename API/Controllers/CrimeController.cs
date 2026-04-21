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
        public async Task<IActionResult> GetById(int id)
            => Ok(await this._crimeService.GetById(id));

        [HttpGet]
        public async Task<IActionResult> Index()
            => Ok(await this._crimeService.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(CreateCrimeRequest request) {
            CrimeResponse crimeResponse = await this._crimeService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = crimeResponse.Id }, crimeResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCrimeRequest request)
            => Ok(await this._crimeService.Update(request));


        [HttpPut("Solved")]
        public async Task<IActionResult> CrimeSolved(int id) {
            await this._crimeService.Solved(id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            await this._crimeService.Delete(id);
            return NoContent();
        }
    }
}