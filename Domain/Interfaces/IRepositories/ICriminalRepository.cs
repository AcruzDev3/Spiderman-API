using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface ICriminalRepository
    {
        Task<Criminal?> GetById(int id);
        Task<List<Criminal>?> GetByIds(List<int> ids);
        Task<List<Criminal>?> GetAll();
        Task<bool> Exists(string name);
        Task<Criminal> Add(Criminal criminal);
        Task<Criminal> Update(Criminal criminal);
        Task Delete(Criminal criminal);
    }
}
