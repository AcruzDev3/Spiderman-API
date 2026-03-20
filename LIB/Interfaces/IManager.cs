
namespace LIB.Interfaces
{
    internal interface IManager<ViewModel, DTO, Model>
    {
        Task<ViewModel> GetById(int id);

        Task<List<ViewModel>> GetAll();

        Task Create(DTO dto);

        Task Delete(int id);

        Task<Model?> Exists(ViewModel viewModel);
    }
}
