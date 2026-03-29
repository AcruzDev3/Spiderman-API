using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface ICrimeRepository
    {
        Task<Crime?> GetById(int id);
        Task<List<Crime>?> GetAll();
        Task<CrimeGrade?> GetGradeByName(string? gradeName);
        Task<CrimeType?> GetTypeByName(string? typeName);
        Task Add(Crime crime);
        Task Update(Crime crime);
        Task Delete(Crime crime);
        Task DeleteRange(List<Crime> models);
        Task<int> SaveChanges();
    }
}
