using Application.Contracts.Requests.CrimeType;
using Application.Contracts.Responses;
using Application.Exceptions;
using Application.Interfaces.IServices;
using Application.Mappers;
using Domain.Interfaces.IRepositories;
using Domain.Models;

namespace Application.Services
{
    public class CrimeTypeService : ICrimeTypeService
    {
        private readonly ICrimeTypeRepository _crimeTypeRepository;

        public CrimeTypeService(ICrimeTypeRepository crimeTypeRepository) {
            this._crimeTypeRepository = crimeTypeRepository;
        }

        public async Task<CrimeTypeResponse> GetById(int id) {
            CrimeType? model = await this._crimeTypeRepository.GetById(id);
            if (model == null) throw new Exception("El tipo de crimen no existe");

            return CrimeTypeMapper.ToResponse(model);
        }

        public async Task<List<CrimeTypeResponse>> GetAll() {
            List<CrimeTypeResponse> viewModels = new List<CrimeTypeResponse>();

            List<CrimeType>? models = await this._crimeTypeRepository.GetAll();
            if (models == null) throw new Exception("No se han podido obtener los tipos de crimen");
            foreach (CrimeType model in models) viewModels.Add(CrimeTypeMapper.ToResponse(model));
            return viewModels;
        }

        public async Task<CrimeTypeResponse> Create(CreateCrimeTypeRequest request) {
            CrimeType model = CrimeTypeMapper.ToModel(request);
            if (await this._crimeTypeRepository.Exists(model.Name))
                throw new Exception("El nombre del tipo de crimen ya está registrado");
            return CrimeTypeMapper.ToResponse(await this._crimeTypeRepository.Add(model));
        }

        public async Task<CrimeTypeResponse> Update(UpdateCrimeTypeRequest request) {
            CrimeType? model = await this._crimeTypeRepository.GetById(request.CrimeTypeId);
            if (model == null) throw new NotFoundException("El tipo de crimen no existe");
            model.ChangeName(request.Name);

            return CrimeTypeMapper.ToResponse(await this._crimeTypeRepository.Update(model));
        }

        public async Task Delete(int id) {
            CrimeType? model = await this._crimeTypeRepository.GetById(id);
            if (model == null) throw new NotFoundException("El tipo de crimen no existe");
            await this._crimeTypeRepository.Delete(model);
        }
    }
}
