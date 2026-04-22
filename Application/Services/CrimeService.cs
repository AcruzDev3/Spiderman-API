using Application.Contracts.Requests.Crime;
using Application.Contracts.Responses;
using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Interfaces.IRepositories;
using Domain.Models;

namespace Application.Services
{
    public class CrimeService : ICrimeService
    {
        private readonly ICrimeRepository _crimeRepository;
        private readonly  ICriminalRepository _criminalRepository;
        private readonly  IUserRepository _userRepository;
        private readonly  IAddressRepository _addressRepository;
        public CrimeService(ICrimeRepository crimeRepository, ICriminalRepository criminalRepository, IUserRepository userRepository, IAddressRepository addressRepository)
        {
            this._crimeRepository = crimeRepository;
            this._criminalRepository = criminalRepository;
            this._userRepository = userRepository;
            this._addressRepository = addressRepository;
        }

        public async Task<CrimeResponse> GetById(int id)
        {
            Crime? model = await this._crimeRepository.GetById(id);
            if(model == null) throw new NotFoundException("No se pudo encontrar el crimen");

            return await this.GetCrime(model);
        }

        public async Task<List<CrimeResponse>> GetAll()
        {
            List<CrimeResponse> viewModels = new List<CrimeResponse>();
            List<Crime>?  models = await this._crimeRepository.GetAll();
            if(models == null) throw new NotFoundException("No se pudieron obtener los crímenes");

            foreach (Crime model in models) viewModels.Add(await this.GetCrime(model));
            return viewModels;
        }

        public async Task<CrimeResponse> Create(CreateCrimeRequest request)
        {
            CrimeGrade? grade = await this._crimeRepository.GetGradeByName(request.GradeId);
            if (grade == null) throw new NotFoundException("El grado del crimen no es válido");

            CrimeType? type = await this._crimeRepository.GetTypeByName(request.TypeId);
            if (type == null) throw new NotFoundException("El tipo del crimen no es válido");

            List<User>? users = await this._userRepository.GetByIds(request.UserIds);
            if (users == null) throw new NotFoundException("El usuario no es válido");

            List<Criminal>? criminals = await this._criminalRepository.GetByIds(request.CriminalIds);
            if (criminals == null) throw new NotFoundException("Los criminales no son válidos");

            Address? address = await this._addressRepository.GetById(request.AddressId);
            if (address == null) throw new NotFoundException("La dirección no es válida");

            Crime model = CrimeMapper.ToModel(request, address, criminals, users, grade, type);

            return await this.GetCrime(await this._crimeRepository.Add(model));
        }
        
        public async Task<CrimeResponse> Update(UpdateCrimeRequest request) {
            CrimeGrade? grade = await this._crimeRepository.GetGradeByName(request.GradeId);
            if (grade == null) throw new NotFoundException("El grado del crimen no es válido");

            CrimeType? type = await this._crimeRepository.GetTypeByName(request.TypeId);
            if (type == null) throw new NotFoundException("El tipo del crimen no es válido");

            List<User>? users = await this._userRepository.GetByIds(request.UserIds);
            if (users == null) throw new NotFoundException("El usuario no es válido");

            List<Criminal>? criminals = await this._criminalRepository.GetByIds(request.CriminalIds);
            if (criminals == null) throw new NotFoundException("Los criminales no son válidos");

            Address? address = await this._addressRepository.GetById(request.AddressId);
            if (address == null) throw new NotFoundException("La dirección no es válida");

            Crime model = CrimeMapper.ToModel(request, address, criminals, users, grade, type);

            return await this.GetCrime(await this._crimeRepository.Update(model));
        }

        public async Task Delete(int id)
        {
            Crime? crime = await this._crimeRepository.GetById(id);
            if (crime == null) throw new NotFoundException("El crimen no existe");

            await this._crimeRepository.Delete(crime);
        }
        
        public async Task Solved(int idCrime)
        {
            Crime? crime = await this._crimeRepository.GetById(idCrime);
            if (crime == null) throw new Exception("El crimen no existe");

            crime.Solved();

            await this._crimeRepository.Update(crime);
        }

        private List<UserResponse> GetUsers(List<User> users) {
            List<UserResponse> responses = new List<UserResponse>();
                foreach (User model in users) {
                    RoleResponse role = RoleMapper.ToResponse(model.Role);
                    responses.Add(UserMapper.ToResponse(model, role));
                }
            return responses;
        }

        private List<CriminalResponse> GetCriminals(List<Criminal> criminals) {
            List<CriminalResponse> responses = new List<CriminalResponse>();
            foreach (Criminal model in criminals) {
                CriminalRiskLevelResponse risk = CriminalRiskLevelMapper.ToResponse(model.Risk);
                responses.Add(CriminalMapper.ToResponse(model, risk));
            }
            return responses;
        }

        private async Task<CrimeResponse> GetCrime(Crime model) {
            List<UserResponse> users = this.GetUsers(model.Users);
            List<CriminalResponse> criminals = this.GetCriminals(model.Criminals);

            AddressResponse address = AddressMapper.ToResponse(model.Address);
            CrimeGradeResponse grade = CrimeGradeMapper.ToResponse(model.Grade);
            CrimeTypeResponse type = CrimeTypeMapper.ToResponse(model.Type);

            return CrimeMapper.ToResponse(model, address, criminals, users, grade, type);
        }
    }
}
