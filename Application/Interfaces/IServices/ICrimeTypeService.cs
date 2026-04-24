using Application.Contracts.Requests.CrimeType;
using Application.Contracts.Responses;

namespace Application.Interfaces.IServices
{
    public interface ICrimeTypeService
    {
        Task<CrimeTypeResponse> GetById(int id);
        Task<List<CrimeTypeResponse>> GetAll();
        Task<CrimeTypeResponse> Create(CreateCrimeTypeRequest request);
        Task<CrimeTypeResponse> Update(UpdateCrimeTypeRequest request);
        Task Delete(int id);
    }
}
