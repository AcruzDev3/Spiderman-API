using API.DTOs;
using LIB.Managers;
using LIB.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    [ApiController, Route("api/[controller]")]
    public class CriminalsController : ControllerBase 
    {
        private readonly CriminalManager _criminalManager;
        public CriminalsController(CriminalManager criminalManager) {
            this._criminalManager = criminalManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            CriminalViewModel? criminal = null;
            try {
                criminal = await _criminalManager.GetById(id);
                if (criminal == null) throw new Exception("El criminal no existe");
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCriminalRequest dto) {
            try {
                await _criminalManager.Create(dto);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }   
            return Ok("El usuario se ha creado con exito");
        }
    }
}
