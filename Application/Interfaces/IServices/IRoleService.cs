using Application.Contracts.Requests.Role;
using Application.Contracts.Responses;

namespace Application.Interfaces.IServices
{
    public interface IRoleService
    {
        Task<RoleResponse> GetById(int id);
        Task<List<RoleResponse>> GetAll();
        Task<RoleResponse> Create(CreateRoleRequest request);
        Task<RoleResponse> Update(UpdateRoleRequest request);
        Task Delete(int id);
    }
}
