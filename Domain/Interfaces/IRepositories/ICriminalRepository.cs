using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface ICriminalRepository
    {
        Task<Criminal?> GetById(int id);
        Task<List<Criminal>?> GetByIds(List<int> ids);
        Task<List<Criminal>?> GetAll();
        Task<List<Crime>?> GetCrimes(Criminal model);
        Task<CriminalRiskLevel?> GetCriminalRiskLevelAsync(int RiskId);
        Task<bool> Exists(string name);
        Task<Criminal> Add(Criminal criminal);
        Task<Criminal> Update(Criminal criminal);
        Task Delete(Criminal criminal);
    }
}
