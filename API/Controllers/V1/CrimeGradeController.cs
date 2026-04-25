using Application.Contracts.Requests.CrimeGrade;
using Application.Contracts.Responses;
using Application.Exceptions;
using Application.Mappers;
using Domain.Interfaces.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiController, Route("api/[controller]")]
    [Authorize]
    public class CrimeGradeController : ControllerBase
    {
        private readonly ICrimeGradeRepository _crimeGradeRepository;

        public CrimeGradeController(ICrimeGradeRepository crimeGradeRepository) {
            this._crimeGradeRepository = crimeGradeRepository;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "HERO")]
        [ProducesResponseType(typeof(CrimeGradeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CrimeGradeResponse> GetById(int id) {
            CrimeGrade? model = await this._crimeGradeRepository.GetById(id);
            if (model == null) throw new Exception("El grado de crimen no existe");

            return CrimeGradeMapper.ToResponse(model);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CrimeGradeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<CrimeGradeResponse>> GetAll() {
            List<CrimeGradeResponse> viewModels = new List<CrimeGradeResponse>();

            List<CrimeGrade>? models = await this._crimeGradeRepository.GetAll();
            if (models == null) throw new Exception("No se han podido obtener los grados de crimen");
            foreach (CrimeGrade model in models) viewModels.Add(CrimeGradeMapper.ToResponse(model));
            return viewModels;
        }

        [HttpPost]
        [Authorize(Roles = "HERO")]
        [ProducesResponseType(typeof(CrimeGradeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CrimeGradeResponse> Create(CreateCrimeGradeRequest request) {
            CrimeGrade model = CrimeGradeMapper.ToModel(request);
            if (await this._crimeGradeRepository.Exists(model.Name))
                throw new Exception("El nombre del grado de crimen ya está registrado");
            return CrimeGradeMapper.ToResponse(await this._crimeGradeRepository.Add(model));
        }

        [HttpPut]
        [Authorize(Roles = "HERO")]
        [ProducesResponseType(typeof(CrimeGradeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CrimeGradeResponse> Update(UpdateCrimeGradeRequest request) {
            CrimeGrade? model = await this._crimeGradeRepository.GetById(request.Id);
            if (model == null) throw new NotFoundException("El grado de crimen no existe");
            model.ChangeName(request.Name);

            return CrimeGradeMapper.ToResponse(await this._crimeGradeRepository.Update(model));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "HERO")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Delete(int id) {
            CrimeGrade? model = await this._crimeGradeRepository.GetById(id);
            if (model == null) throw new NotFoundException("El grado de crimen no existe");
            await this._crimeGradeRepository.Delete(model);
        }
    }
}
