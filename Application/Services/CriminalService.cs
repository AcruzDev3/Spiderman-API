using Application.Constans;
using Application.Contracts.Requests.Criminal;
using Application.Contracts.Responses;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.IServices;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Interfaces.IRepositories;
using Domain.Models;

namespace Application.Services
{
    public class CriminalService : ICriminalService
    {
        private readonly ICriminalRepository _criminalRepository;
        private readonly ICrimeRepository _crimeRepository;
        private readonly IAzureImageService _azureImageService;
        public CriminalService(ICriminalRepository criminalRepository, ICrimeRepository _crimeRepository,
            IAzureImageService azureImageService)
        {
            this._criminalRepository = criminalRepository;
            this._crimeRepository = _crimeRepository;
            this._azureImageService = azureImageService;
        }

        public async Task<CriminalResponse> GetById(int id)
        {
            Criminal? model = await this._criminalRepository.GetById(id);
            if (model == null) throw new NotFoundException("El criminal no existe");

            CriminalRiskLevelResponse risk = CriminalRiskLevelMapper.ToResponse(model.Risk);
            return CriminalMapper.ToResponse(model, risk);
        }

        public async Task<List<CriminalResponse>> GetAll()
        {
        List<CriminalResponse> viewModels = new List<CriminalResponse>();
            List<Criminal>? models = await this._criminalRepository.GetAll();
            if (models == null) throw new NotFoundException("No se han podido obtener los criminales");

            foreach (Criminal model in models) {
                CriminalRiskLevelResponse risk = CriminalRiskLevelMapper.ToResponse(model.Risk);
                viewModels.Add(CriminalMapper.ToResponse(model, risk));
            }
            return viewModels;
        }

        public async Task<CriminalResponse> Create(CreateCriminalRequest request, string pathImageProfile)
        {
            CriminalRiskLevel? riskLevel = await this._criminalRepository.GetCriminalRiskLevelAsync(request.RiskId);
            if (riskLevel == null) throw new NotFoundException("El nivel de riesgo no es válido");

            string urlImage = DefaultImagesPath.User;
            if (request.Image != null) {
                urlImage = await this._azureImageService.UploadImageAsync(
                    request.Image.OpenReadStream(),
                    FolderImageEnum.Criminals.ToString().ToLower(),
                    request.Image.ContentType
                );
            }

            Criminal model = CriminalMapper.ToModel(request, riskLevel, pathImageProfile);
            if(await this._criminalRepository.Exists(request.Name))
                throw new Exception("El criminal ya existe");

            return CriminalMapper.ToResponse(
                await this._criminalRepository.Add(model),
                CriminalRiskLevelMapper.ToResponse(riskLevel)
            );
        }

        public async Task<CriminalResponse> Update(UpdateCriminalRequest request, string pathImageProfile) {
            CriminalRiskLevel? riskLevel = await this._criminalRepository.GetCriminalRiskLevelAsync(request.RiskId);
            if (riskLevel == null) throw new NotFoundException("El nivel de riesgo no es válido");

            Criminal? model = await this._criminalRepository.GetById(request.Id);
            if(model == null) throw new NotFoundException("El criminal no existe");

            string urlImage = model.Image;

            if (request.Image != null) {
                urlImage = await this._azureImageService.UploadImageAsync(
                    request.Image.OpenReadStream(),
                    FolderImageEnum.Criminals.ToString().ToLower(),
                    request.Image.ContentType
                );
                await this._azureImageService.DeleteAsync(model.Image);
            }

            Criminal newCriminal = CriminalMapper.ToModel(request, riskLevel, pathImageProfile);
            
            return CriminalMapper.ToResponse(
                await this._criminalRepository.Update(newCriminal),
                CriminalRiskLevelMapper.ToResponse(riskLevel)
            );
        }

        public async Task Delete(int id)
        {
            Criminal? criminal = await this._criminalRepository.GetById(id);
            if (criminal == null) throw new NotFoundException("No se pudo encontrar el criminal");

            List<Crime>? crimes = await this._criminalRepository.GetCrimes(criminal);
                
            await this._criminalRepository.Delete(criminal);
                
            if (crimes == null) return;
            List<Crime>? crimesWithoutCriminals = crimes
                .Where(c => !c.Criminals.Any())
                .ToList();

            if (crimesWithoutCriminals.Any())
                await this._crimeRepository.DeleteRange(crimesWithoutCriminals);
        }
    }
}
