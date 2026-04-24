using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface ICrimeTypeRepository
    {
        Task<CrimeType?> GetById(int id);
        Task<List<CrimeType>?> GetAll();
        Task<CrimeType> Add(CrimeType model);
        Task<CrimeType> Update(CrimeType model);
        Task Delete(CrimeType model);
        Task<bool> Exists(string name);
    }
}
