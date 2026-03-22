using LIB.Interfaces.IRepositories;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LIB.Repositories
{
    public class CrimeRepository : ICrimeRepository
    {
        private readonly SpidermanContext _context;

        public CrimeRepository(SpidermanContext context)
        {
            this._context = context;
        }

        public async Task<Crime?> GetById(int id) {
            return await this._context.Crimes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CrimeId == id);
        }

        public async Task<List<Crime>?> GetAll() {
            return await this._context.Crimes
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<bool> Exists(CrimeViewModel viewModel) {
            return await this._context.Crimes.AnyAsync(m =>
                m.Grade.Name.Equals(viewModel.Grade, StringComparison.OrdinalIgnoreCase) &&
                m.Type.Name.Equals(viewModel.Type, StringComparison.OrdinalIgnoreCase) &&
                m.DateStart == viewModel.Start &&
                m.DateEnd == viewModel.End &&
                m.Status == viewModel.Status &&
                m.AddressId == viewModel.Address.Id
            );
        }
        public async Task<CrimeGrade?> GetGradeByName(string? gradeName) {
            return await this._context.CrimeGrades
                .FirstOrDefaultAsync(m => m.Name.Equals(gradeName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<CrimeType?> GetTypeByName(string? typeName) {
            return await this._context.CrimeTypes
                .FirstOrDefaultAsync(m => m.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task Add(Crime crime) {
            await this._context.Crimes.AddAsync(crime);
        }

        public void Update(Crime crime) {
            this._context.Crimes.Update(crime);
        }

        public void Delete(Crime crime) {
            this._context.Crimes.Remove(crime);
        }

        public void DeleteRange(List<Crime> models) {
            this._context.Crimes.RemoveRange(models);
        }
        public async Task<int> SaveChanges() {
            return await this._context.SaveChangesAsync();
        }

    }
}
