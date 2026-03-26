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
        void Update(Crime crime);
        void Delete(Crime crime);
        void DeleteRange(List<Crime> models);
        Task<int> SaveChanges();
    }
}
