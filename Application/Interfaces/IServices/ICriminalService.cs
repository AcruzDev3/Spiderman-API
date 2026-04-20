using Application.Contracts.Requests.Criminal;
using Application.Contracts.Responses;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface ICriminalService
    {
        Task<CriminalResponse> GetById(int id);
        Task<List<CriminalResponse>> GetAll();
        Task<CriminalResponse> Create(CreateCriminalRequest request, string pathImageProfile);
        Task<CriminalResponse> Update(UpdateCriminalRequest request, string pathImageProfile);
        Task Delete(int id);
    }
}
