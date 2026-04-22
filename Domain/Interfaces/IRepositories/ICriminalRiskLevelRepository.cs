using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface ICriminalRiskLevelRepository
    {
        Task<CriminalRiskLevel?> GetById(int id);
        Task<List<CriminalRiskLevel>?> GetAll();
        Task<CriminalRiskLevel> Add(CriminalRiskLevel model);
        Task<CriminalRiskLevel> Update(CriminalRiskLevel model);
        Task Delete(CriminalRiskLevel model);
        Task<bool> Exists(string name);
    }
}
