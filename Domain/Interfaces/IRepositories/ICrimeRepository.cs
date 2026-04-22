using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface ICrimeRepository
    {
        Task<Crime?> GetById(int id);
        Task<List<Crime>?> GetAll();
        Task<CrimeGrade?> GetGradeByName(int gradeName);
        Task<CrimeType?> GetTypeByName(int typeName);
        Task<Crime> Add(Crime crime);
        Task<Crime> Update(Crime crime);
        Task<List<Crime>?> GetCrimesOfCriminal(int criminalId);
        Task Delete(Crime crime);
        Task DeleteRange(List<Crime> models);
    }
}
