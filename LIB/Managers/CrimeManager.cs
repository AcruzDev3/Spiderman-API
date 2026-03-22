using LIB.Models;
using LIB.ViewModels;
using LIB.Interfaces.IManagers;
using LIB.Interfaces.IRepositories;
using LIB.DTOs.Crime;

namespace LIB.Managers
{
    public class CrimeManager : ICrimeManager
    {
        private readonly ICrimeRepository _crimeRepository;
        private readonly  ICriminalRepository _criminalRepository;
        private readonly  IUserRepository _userRepository;
        private readonly  IAddressRepository _addressRepository;
        public CrimeManager(ICrimeRepository crimeRepository, ICriminalRepository criminalRepository, IUserRepository userRepository, IAddressRepository addressRepository)
        {
            this._crimeRepository = crimeRepository;
            this._criminalRepository = criminalRepository;
            this._userRepository = userRepository;
            this._addressRepository = addressRepository;
        }

        public async Task<CrimeViewModel> GetById(int id)
        {
            CrimeViewModel? viewModel = null;
            try {

                Crime? model = await this._crimeRepository.GetById(id);
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
                List<Crime>?  models = await this._crimeRepository.GetAll();

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
                if(dto == null) throw new Exception("El crimen no es válido");

                CrimeGrade? grade = await this._crimeRepository.GetGradeByName(dto.GradeName);
                if (grade == null) throw new Exception("El grado del crimen no es válido");

                CrimeType? type = await this._crimeRepository.GetTypeByName(dto.TypeName);
                if (type == null) throw new Exception("El tipo del crimen no es válido");

                List<User>? users = await this._userRepository.GetByIds(dto.UserIds);
                if(users == null) throw new Exception("El usuario no es válido");

                List<Criminal>? criminals = await this._criminalRepository.GetByIds(dto.CriminalIds);
                if (criminals == null) throw new Exception("Los criminales no son válidos");

                Address? address = await this._addressRepository.GetById(dto.AddressId);
                if (address == null) throw new Exception("La dirección no es válida");

                CrimeViewModel viewModel = new CrimeViewModel(dto);
                if(viewModel == null) throw new Exception("El crimen no es válido");

                Crime? model = new Crime(users, criminals, dto.Description, grade, type, address);
                if(model == null) throw new Exception("El crimen no es válido");

                await this._crimeRepository.Add(model);
                int rowsAffected = await this._crimeRepository.SaveChanges();
                if (rowsAffected != 1) throw new Exception("No se pudo crear el crimen");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateCrimeRequest dto) {
            try {
                if(dto == null) throw new Exception("El crimen no es válido");

                Crime? model = await this._crimeRepository.GetById(dto.Id);
                if(model == null) throw new Exception("El crimen no existe");

                this._crimeRepository.Update(model);
                int rowsAffected = await this._crimeRepository.SaveChanges();

                if (rowsAffected != 1) throw new Exception("No se pudo actualizar el crimen");
            } catch(Exception) {
                throw;
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                Crime? crime = await this._crimeRepository.GetById(id);
                if (crime == null) throw new Exception("El crimen no existe");

                Address? address = await this._addressRepository.GetById(crime.AddressId);
                if (address == null) throw new Exception("La dirección asociada al crimen no existe");

                this._addressRepository.Delete(address);
                await this._addressRepository.SaveChanges();

                this._crimeRepository.Delete(crime);

                int rowsAffected = await this._crimeRepository.SaveChanges();
                if(rowsAffected != 1) throw new Exception("No se pudo eliminar el crimen");
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
                Crime? crime = await this._crimeRepository.GetById(idCrime);
                if (crime == null) throw new Exception("No se pudo encontrar el crimen");

                crime.Status = true;
                crime.DateEnd = DateTime.Now;

                this._crimeRepository.Update(crime);
                rowsAffected = await this._crimeRepository.SaveChanges();
                if(rowsAffected != 1) throw new Exception("No se pudo actualizar el crimen");
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }
    }
}
