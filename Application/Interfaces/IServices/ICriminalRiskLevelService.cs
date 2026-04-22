using Application.Contracts.Requests.CriminalRiskLevel;
using Application.Contracts.Responses;

namespace Application.Interfaces.IServices
{
    public interface ICriminalRiskLevelService
    {
        Task<CriminalRiskLevelResponse> GetById(int id);
        Task<List<CriminalRiskLevelResponse>> GetAll();
        Task<CriminalRiskLevelResponse> Create(CreateCriminalRiskLevelRequest request);
        Task<CriminalRiskLevelResponse> Update(UpdateCriminalRiskLevelRequest request);
        Task Delete(int id);
    }
}
