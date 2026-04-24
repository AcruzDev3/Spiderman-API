using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface ICrimeGradeRepository
    {
        Task<CrimeGrade?> GetById(int id);
        Task<List<CrimeGrade>?> GetAll();
        Task<CrimeGrade> Add(CrimeGrade model);
        Task<CrimeGrade> Update(CrimeGrade model);
        Task Delete(CrimeGrade model);
        Task<bool> Exists(string name);
    }
}
