using Application.Contracts.Requests.Crime;
using Application.Contracts.Responses;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface ICrimeService
    {
        Task<CrimeResponse> GetById(int id);
        Task<List<CrimeResponse>> GetAll();
        Task<CrimeResponse> Create(CreateCrimeRequest request);
        Task<CrimeResponse> Update(UpdateCrimeRequest request);
        Task Delete(int id);
        Task Solved(int idCrime);
    }
}
