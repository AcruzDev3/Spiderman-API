using Application.Contracts.Requests.CriminalRiskLevel;
using Application.Contracts.Responses;
using Application.Exceptions;
using Application.Interfaces.IServices;
using Application.Mappers;
using Domain.Interfaces.IRepositories;
using Domain.Models;

namespace Application.Services
{
    public class CriminalRiskLevelService : ICriminalRiskLevelService
    {
        private readonly ICriminalRiskLevelRepository _criminalRiskLevelRepository;

        public CriminalRiskLevelService(ICriminalRiskLevelRepository criminalRiskLevelRepository) {
            this._criminalRiskLevelRepository = criminalRiskLevelRepository;
        }

        public async Task<CriminalRiskLevelResponse> GetById(int id) {
            CriminalRiskLevel? model = await this._criminalRiskLevelRepository.GetById(id);
            if (model == null) throw new Exception("El nivel de peligrosidad del criminal no existe");

            return CriminalRiskLevelMapper.ToResponse(model);
        }

        public async Task<List<CriminalRiskLevelResponse>> GetAll() {
            List<CriminalRiskLevelResponse> viewModels = new List<CriminalRiskLevelResponse>();

            List<CriminalRiskLevel>? models = await this._criminalRiskLevelRepository.GetAll();
            if (models == null) throw new Exception("No se han podido obtener los niveles de peligrosidad de los criminal");
            foreach (CriminalRiskLevel model in models) viewModels.Add(CriminalRiskLevelMapper.ToResponse(model));
            return viewModels;
        }

        public async Task<CriminalRiskLevelResponse> Create(CreateCriminalRiskLevelRequest request) {
            CriminalRiskLevel model = CriminalRiskLevelMapper.ToModel(request);
            if (await this._criminalRiskLevelRepository.Exists(model.Name))
                throw new ConflictException("El nombre del nivel de peligrosidad del criminal ya está registrado");
            return CriminalRiskLevelMapper.ToResponse(await this._criminalRiskLevelRepository.Add(model));
        }

        public async Task<CriminalRiskLevelResponse> Update(UpdateCriminalRiskLevelRequest request) {
            CriminalRiskLevel? model = await this._criminalRiskLevelRepository.GetById(request.Id);
            if (model == null) throw new NotFoundException("El nivel de peligrosidad del criminal no existe");
            model.ChangeName(request.Name);

            return CriminalRiskLevelMapper.ToResponse(await this._criminalRiskLevelRepository.Update(model));
        }

        public async Task Delete(int id) {
            CriminalRiskLevel? model = await this._criminalRiskLevelRepository.GetById(id);
            if (model == null) throw new NotFoundException("El nivel depeligrosidad del criminal no existe");
            await this._criminalRiskLevelRepository.Delete(model);   
        }
    }
}
