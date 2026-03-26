using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface ICriminalRepository
    {
        Task<Criminal?> GetById(int id);
        Task<List<Criminal>?> GetByIds(List<int> ids);
        Task<List<Criminal>?> GetAll();
        Task<List<Crime>?> GetCrimes(Criminal model);
        Task<CriminalRiskLevel?> GetCriminalRiskLevelAsync(string? RiskLevel);
        Task Add(Criminal criminal);
        void Update(Criminal criminal);
        void Delete(Criminal criminal);
        Task<int> SaveChanges();
    }
}
