using LIB.Interfaces;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;
using LIB.DTOs;

namespace LIB.Managers
{
    public class CrimeManager : IManager<CrimeViewModel, CreateCrimeRequest, Crime>
    {
        private readonly SpidermanContext _context;
        private readonly AddressManager _addressManager;
        private readonly CriminalManager _criminalManager;
        public CrimeManager(SpidermanContext context, AddressManager addressManager, CriminalManager criminalManager)
        {
            this._context = context;
            this._addressManager = addressManager;
            this._criminalManager = criminalManager;
        }

        public async Task<CrimeViewModel> GetById(int id)
        {
            CrimeViewModel? viewModel = null;
            try {

                Crime? model = await this.GetModel(id);
                if(model == null) throw new Exception("No se pudo encontrar el crimen");
                viewModel = new CrimeViewModel(model);
            }
            catch (Exception) {
                throw;
            }
            return viewModel;
        }
        public async Task<List<CrimeViewModel>> GetAll()
        {
            List<CrimeViewModel> viewModels = new List<CrimeViewModel>();
            try
            {
                List<Crime>?  models = await this.GetAllModels();

                if(models == null) throw new Exception("No se han podido obtener los crimenes");

                foreach (Crime model in models) viewModels.Add(new CrimeViewModel(model));
            }
            catch (Exception)
            {
                throw;
            }
            return viewModels;
        }

        public async Task Create(CreateCrimeRequest dto)
        {
            try
            {
                CrimeGrade? grade = await this.VerifyGradeCrime(dto.GradeName);
                if (grade == null) throw new Exception("El grado del crimen no es válido");

                CrimeType? type = await this.VerifyTypeCrime(dto.TypeName);
                if (type == null) throw new Exception("El tipo del crimen no es válido");


                AddressViewModel addressViewModel = await this._addressManager.GetById(dto.AddressId);
                if (addressViewModel == null) throw new Exception("La dirección no es válida");

                List<CriminalViewModel> criminals = new List<CriminalViewModel>();
                foreach(int id in dto.CriminalsIds) {
                    CriminalViewModel? criminalViewModel = await this._criminalManager.GetById(id);
                    if(criminalViewModel == null) throw new Exception("El criminal con id " + id + " no es válido");
                    else criminals.Add(criminalViewModel);
                }
                
                CrimeViewModel viewModel = new CrimeViewModel(dto, addressViewModel, grade, type, criminals);
                if(viewModel == null) throw new Exception("El crimen no es válido");

                Crime? model = new Crime(viewModel, grade, type, addressViewModel);
                if(model == null) throw new Exception("El crimen no es válido");

                await this._context.Crimes.AddAsync(model);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new Exception("No se pudo crear el crimen");
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
                if(id < 1) throw new Exception("El crimen no es válido");
                Crime? crime = await this.GetModel(id);
                if (crime == null) throw new Exception("El crimen no existe");

                AddressViewModel? address = await this._addressManager.GetById(crime.AddressId);
                if (address == null) throw new Exception("La dirección asociada al crimen no existe");

                await this._addressManager.Delete(address.Id);

                this._context.Crimes.Remove(crime);
                int rowsAffected = await this._context.SaveChangesAsync();
                if(rowsAffected != 1) throw new Exception("No se pudo eliminar el crimen");
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<int> DeleteAllCrimesAssociatedWhithId(int id)
        {
            int rowsAffected = -1;
            try
            {
                this._context.Crimes.RemoveRange(_context.Crimes.Where(c => c.Criminals.Any(cr => cr.CriminalId == id)));
            }
            catch(Exception)
            {
                throw;
            }
            return rowsAffected;
        }
        public async Task<Crime?> Exists(CrimeViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new Exception("El modelo vista del crimen no es válido");

                return await this._context.Crimes
                    .FirstOrDefaultAsync(m =>
                        m.Grade.Name.Equals(viewModel.Grade, StringComparison.CurrentCultureIgnoreCase) &&
                        m.Type.Name.Equals(viewModel.Type, StringComparison.CurrentCultureIgnoreCase) &&
                        m.DateStart == viewModel.Start &&
                        m.DateEnd == viewModel.End &&
                        m.Status == viewModel.Status &&
                        m.AddressId == viewModel.Address.Id
                    );
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Solved(int idCrime)
        {
            int rowsAffected = 0;
            try
            {
                if (idCrime <= 0) return 0;
                Crime? crime = await GetModel(idCrime);
                if (crime == null) throw new Exception("No se pudo encontrar el crimen");

                crime.Status = true;
                crime.DateEnd = DateTime.Now;

                this._context.Crimes.Update(crime);
                rowsAffected = await this._context.SaveChangesAsync();
                if(rowsAffected != 1) throw new Exception("No se pudo actualizar el crimen");
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }

        private async Task<CrimeGrade> VerifyGradeCrime(string gradeName)
        {
            CrimeGrade? model = null;
            try
            {
                model = await this._context.CrimeGrades
                    .FirstOrDefaultAsync(m => m.Name.Equals(gradeName, StringComparison.CurrentCultureIgnoreCase));
                if (model == null) throw new Exception("El grado del crimen no es válido");
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        private async Task<CrimeType> VerifyTypeCrime(string typeName)
        {
            CrimeType? model = null;
            try
            {
                model = await this._context.CrimeTypes.AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Name.Equals(typeName, StringComparison.CurrentCultureIgnoreCase));
                if (model == null) throw new Exception("El tipo del crimen no es válido");
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        private async Task<Crime?> GetModel(int id)
        {
            if (id <= 0) return null;
            return await this._context.Crimes.AsNoTracking().FirstOrDefaultAsync(c => c.CrimeId == id);
        }
        private async Task<List<Crime>> GetAllModels()
        {
            return await this._context.Crimes.AsNoTracking().ToListAsync();
        }
    }
}
