using API.DTOs;
using LIB.Interfaces;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LIB.Managers
{
    public class CriminalManager : IManager<CriminalViewModel, CreateCriminalRequest, Criminal>
    {
        private readonly SpidermanContext _context;
        private readonly CrimeManager _crimeManager;
        public CriminalManager(SpidermanContext context, CrimeManager crimeManager)
        {
            _context = context;
            _crimeManager = crimeManager;
        }
        public async Task<CriminalViewModel> GetById(int id)
        {
            CriminalViewModel? viewModel = null;
            try
            {
                Criminal? model = await this.GetModel(id);

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
                List<Criminal>? models = await this.GetAllModels();
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
                CriminalRiskLevel? riskLevel = await VerifyRiskCriminal(dto.Risk);
                if (riskLevel == null) throw new Exception("El nivel de riesgo no es válido");

                CriminalViewModel viewModel = new CriminalViewModel(dto, riskLevel);
                if(await this.Exists(viewModel) == null) throw new Exception("El criminal ya existe");

                Criminal? model = new Criminal(viewModel, riskLevel);

                if(model == null) throw new Exception("No se pudo crear el criminal");

                await _context.Criminals.AddAsync(model);
                int rowsAffected = await this._context.SaveChangesAsync();

                if(rowsAffected != 1) throw new Exception("No se pudo crear el criminal");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                if (id < 1) throw new Exception("El id del criminal no es válido");
                
                Criminal? criminal = await this.GetModel(id);
                if (criminal == null) throw new Exception("No se pudo encontrar el criminal");

                int rowsDeleted = await this._crimeManager.DeleteAllCrimesAssociatedWhithId(id);
                if (rowsDeleted < 0) throw new Exception("No se pudieron eliminar los crímenes asociados al criminal");

                this._context.Criminals.Remove(criminal);
                int rowsAffected = await this._context.SaveChangesAsync();

                if (rowsAffected != 1) throw new Exception("No se pudo eliminar el criminal");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Criminal?> Exists(CriminalViewModel viewModel)
        {
            if (viewModel == null) throw new Exception("El modelo de vista del criminal es nulo");
            try
            {
                return await this._context.Criminals
                    .FirstOrDefaultAsync(m => m.Name.Equals(viewModel.Name, StringComparison.CurrentCultureIgnoreCase) &&
                        m.Risk.Name.Equals(viewModel.Risk, StringComparison.CurrentCultureIgnoreCase) &&
                        m.CriminalSince == viewModel.Since
                    );
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<CriminalRiskLevel?> VerifyRiskCriminal(string riskName)
        {
            try
            {
                return await this._context.CriminalRiskLevels.FirstOrDefaultAsync(
                    m => m.Name.Equals(riskName, StringComparison.CurrentCultureIgnoreCase)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Criminal?> GetModel(int id) {
            return await this._context.Criminals.FirstOrDefaultAsync(m => m.CriminalId == id);
        }

        private async Task<List<Criminal>?> GetAllModels() {
            return await this._context.Criminals.ToListAsync();
        }
    }
}
