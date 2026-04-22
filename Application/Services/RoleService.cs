using Application.Contracts.Requests.Role;
using Application.Contracts.Responses;
using Application.Exceptions;
using Application.Mappers;
using Domain.Interfaces.IRepositories;
using Domain.Models;

namespace Application.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public async Task<RoleResponse> GetById(int id)
        {
            Role? model = await this._roleRepository.GetById(id);
            if (model == null) throw new Exception("El rol no existe");

            return RoleMapper.ToResponse(model);
        }

        public async Task<List<RoleResponse>> GetAll()
        {
            List<RoleResponse> viewModels = new List<RoleResponse>();

            List<Role>? models = await this._roleRepository.GetAll();
            if (models == null) throw new Exception("No se han podido obtener los roles");
            foreach (Role model in models) viewModels.Add(RoleMapper.ToResponse(model));
            return viewModels;
        }

        public async Task<RoleResponse> Create(CreateRoleRequest request)
        {
            Role model = RoleMapper.ToModel(request);
            if (await this._roleRepository.Exists(model.Name))
                throw new Exception("El nombre del rol ya está registrado");
            return RoleMapper.ToResponse(await this._roleRepository.Add(model));
        }

        public async Task<RoleResponse> Update(int id, UpdateRoleRequest request)
        {
            Role? model = await this._roleRepository.GetById(id);
            if (model == null) throw new NotFoundException("El rol no existe");
            model.ChangeName(request.Name);
            
            return RoleMapper.ToResponse(await this._roleRepository.Update(model));
        }

        public async Task Delete(int id)
        {
            Role? model = await this._roleRepository.GetById(id);
            if (model == null) throw new NotFoundException("El rol no existe");
        }
    }
}
