using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetById(int id);
        Task<List<Role>?> GetAll();
        Task<Role?> GetNeighbourRole();
        Task<Role> Add(Role model);
        Task<Role> Update(Role model);
        Task Delete(Role model);
        Task<bool> Exists(string name);
    }
}
