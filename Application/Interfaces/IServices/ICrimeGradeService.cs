using Application.Contracts.Requests.CrimeGrade;
using Application.Contracts.Responses;

namespace Application.Interfaces.IServices
{
    public interface ICrimeGradeService
    {
        Task<CrimeGradeResponse> GetById(int id);
        Task<List<CrimeGradeResponse>> GetAll();
        Task<CrimeGradeResponse> Create(CreateCrimeGradeRequest request);
        Task<CrimeGradeResponse> Update(UpdateCrimeGradeRequest request);
        Task Delete(int id);
    }
}
