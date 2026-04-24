using Application.Contracts.Requests.CrimeType;
using Application.Contracts.Responses;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CrimeTypeController : Controller
    {
        private readonly ICrimeTypeService _crimeTypeService;
        public CrimeTypeController(ICrimeTypeService crimeTypeService) {
            this._crimeTypeService = crimeTypeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await this._crimeTypeService.GetById(id));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await this._crimeTypeService.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(CreateCrimeTypeRequest request) {
            CrimeTypeResponse crimeTypeResponse = await this._crimeTypeService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = crimeTypeResponse.Id }, crimeTypeResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCrimeTypeRequest request)
            => Ok(await this._crimeTypeService.Update(request));

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            await this._crimeTypeService.Delete(id);
            return NoContent();
        }
    }
}
