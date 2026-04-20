using Application.Contracts.Requests.Criminal;
using Application.Contracts.Responses;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    [ApiController, Route("api/[controller]")]
    public class CriminalController : ControllerBase
    {
        private readonly ICriminalService _criminalService;
        public CriminalController(ICriminalService criminalService) {
            this._criminalService = criminalService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
            => Ok(await this._criminalService.GetById(id));

        [HttpGet]
        public async Task<IActionResult> GetAll() 
            => Ok(await this._criminalService.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(CreateCriminalRequest request) {
            CriminalResponse criminalResponse = await this._criminalService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = criminalResponse.Id }, criminalResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCriminalRequest request)
            => Ok(await this._criminalService.Update(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await this._criminalService.Delete(id);
            return NoContent();
        }
    }
}
