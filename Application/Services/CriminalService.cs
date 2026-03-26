using LIB.DTOs.Criminal;
using LIB.Interfaces.IManagers;
using LIB.Interfaces.IRepositories;
using LIB.Models;
using LIB.ViewModels;

namespace Application.Services
{
    public class CriminalService : ICriminalManager
    {
        private readonly ICriminalRepository _criminalRepository;
        private readonly ICrimeRepository _crimeRepository;
        public CriminalService(ICriminalRepository criminalRepository, ICrimeRepository _crimeRepository)
        {
            this._criminalRepository = criminalRepository;
            this._crimeRepository = _crimeRepository;
        }

        public async Task<CriminalViewModel> GetById(int id)
        {
            CriminalViewModel? viewModel = null;
            try
            {
                Criminal? model = await this._criminalRepository.GetById(id);

                if (model == null) throw new Exception("No se pudo encontrar el criminal");

                viewModel = new CriminalViewModel(model);
            }
            catch (Exception)
            {
                throw;
            }
            return viewModel;
        }

        public async Task<List<CriminalViewModel>> GetAll()
        {
            List<CriminalViewModel> viewModels = new List<CriminalViewModel>();
            try
            {
                List<Criminal>? models = await this._criminalRepository.GetAll();
                if (models == null) throw new Exception("No se han podido obtener los criminales");

                foreach (Criminal model in models) viewModels.Add(new CriminalViewModel(model));
            }
            catch (Exception)
            {
                throw;
            }
            return viewModels;
        }

        public async Task Create(CreateCriminalRequest dto)
        {
            try
            {
                CriminalRiskLevel? riskLevel = await this._criminalRepository.GetCriminalRiskLevelAsync(dto.Risk);
                if (riskLevel == null) throw new Exception("El nivel de riesgo no es válido");

                CriminalViewModel viewModel = new CriminalViewModel(dto, riskLevel);
                if(await this._criminalRepository.Exists(viewModel) == null) throw new Exception("El criminal ya existe");

                Criminal? model = new Criminal(viewModel, riskLevel);

                if(model == null) throw new Exception("No se pudo crear el criminal");

                await this._criminalRepository.Add(model);
                int rowsAffected = await this._criminalRepository.SaveChanges();

                if(rowsAffected != 1) throw new Exception("No se pudo crear el criminal");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateCriminalRequest dto) {
            try {
                if(dto == null) throw new Exception("El criminal no es válido");

                Criminal? model = await this._criminalRepository.GetById(dto.Id);
                if(model == null) throw new Exception("El criminal no existe");

                this._criminalRepository.Update(model);
                int rowsAffected = await this._criminalRepository.SaveChanges();

                if(rowsAffected != 1) throw new Exception("No se ha podido actualizar al criminal");

            } catch(Exception) {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {   
                Criminal? criminal = await this._criminalRepository.GetById(id);
                if (criminal == null) throw new Exception("No se pudo encontrar el criminal");

                List<Crime>? crimes = await this._criminalRepository.GetCrimes(criminal);
                
                this._criminalRepository.Delete(criminal);
                
                int rowsAffected = await this._criminalRepository.SaveChanges();
                if(rowsAffected != 1) throw new Exception("No se pudo eliminar el criminal");


                if (crimes == null) return;
                List<Crime>? crimesWithoutCriminals = crimes
                    .Where(c => !c.Criminals.Any())
                    .ToList();

                if (crimesWithoutCriminals.Any()) {
                    this._crimeRepository.DeleteRange(crimesWithoutCriminals);
                    await this._criminalRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
