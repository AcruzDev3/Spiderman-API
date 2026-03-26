using LIB.Models;
using LIB.ViewModels;

namespace Application.Interfaces.IRepositories
{
    public interface ICrimeRepository
    {
        Task<Crime?> GetById(int id);
        Task<List<Crime>?> GetAll();
        Task<bool> Exists(CrimeViewModel viewModel);
        Task<CrimeGrade?> GetGradeByName(string? gradeName);
        Task<CrimeType?> GetTypeByName(string? typeName);
        Task Add(Crime crime);
        void Update(Crime crime);
        void Delete(Crime crime);
        void DeleteRange(List<Crime> models);
        Task<int> SaveChanges();
    }
}
