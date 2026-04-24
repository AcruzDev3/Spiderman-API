using Application.Contracts.Requests.CrimeGrade;
using Application.Contracts.Responses;
using Application.Exceptions;
using Application.Mappers;
using Domain.Interfaces.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CrimeGradeController : Controller
    {
        private readonly ICrimeGradeRepository _crimeGradeRepository;

        public CrimeGradeController(ICrimeGradeRepository crimeGradeRepository) {
            this._crimeGradeRepository = crimeGradeRepository;
        }

        public async Task<CrimeGradeResponse> GetById(int id) {
            CrimeGrade? model = await this._crimeGradeRepository.GetById(id);
            if (model == null) throw new Exception("El grado de crimen no existe");

            return CrimeGradeMapper.ToResponse(model);
        }

        public async Task<List<CrimeGradeResponse>> GetAll() {
            List<CrimeGradeResponse> viewModels = new List<CrimeGradeResponse>();

            List<CrimeGrade>? models = await this._crimeGradeRepository.GetAll();
            if (models == null) throw new Exception("No se han podido obtener los grados de crimen");
            foreach (CrimeGrade model in models) viewModels.Add(CrimeGradeMapper.ToResponse(model));
            return viewModels;
        }

        public async Task<CrimeGradeResponse> Create(CreateCrimeGradeRequest request) {
            CrimeGrade model = CrimeGradeMapper.ToModel(request);
            if (await this._crimeGradeRepository.Exists(model.Name))
                throw new Exception("El nombre del grado de crimen ya está registrado");
            return CrimeGradeMapper.ToResponse(await this._crimeGradeRepository.Add(model));
        }

        public async Task<CrimeGradeResponse> Update(UpdateCrimeGradeRequest request) {
            CrimeGrade? model = await this._crimeGradeRepository.GetById(request.Id);
            if (model == null) throw new NotFoundException("El grado de crimen no existe");
            model.ChangeName(request.Name);

            return CrimeGradeMapper.ToResponse(await this._crimeGradeRepository.Update(model));
        }

        public async Task Delete(int id) {
            CrimeGrade? model = await this._crimeGradeRepository.GetById(id);
            if (model == null) throw new NotFoundException("El grado de crimen no existe");
            await this._crimeGradeRepository.Delete(model);
        }
    }
}
