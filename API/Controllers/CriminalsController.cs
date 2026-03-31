using Application.Contracts.Requests.Criminal;
using Application.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    [ApiController, Route("api/[controller]")]
    public class CriminalsController : ControllerBase
    {
        private readonly ICriminalService _criminalService;
        public CriminalsController(ICriminalService criminalService) {
            this._criminalService = criminalService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            Criminal? criminal = null;

            criminal = await this._criminalService.GetById(id);
            if (criminal == null) return NotFound("El criminal no existe");

            return Ok(criminal);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            List<Criminal> criminals = await this._criminalService.GetAll();
            if (criminals.Count == 0) return NotFound("No hay criminales registrados");
            return Ok(criminals);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCriminalRequest request) {
            try {
                await this._criminalService.Create(request);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok("El usuario se ha creado con exito");
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateCriminalRequest request) {
            try {
                await this._criminalService.Update(request);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok("El usuario se ha actualizado con exito");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                await this._criminalService.Delete(id);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok("El usuario se ha eliminado con exito");
        }
    }
}
