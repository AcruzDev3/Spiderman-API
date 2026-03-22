using LIB.Interfaces.IRepositories;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LIB.Repositories
{
    public class CriminalRepository : ICriminalRepository
    {
        private readonly SpidermanContext _context;

        public CriminalRepository(SpidermanContext context) {
            this._context = context;
        }

        public async Task<Criminal?> GetById(int id) {
            return await this._context.Criminals
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CriminalId == id);
        }

        public async Task<List<Criminal>?> GetByIds(List<int> ids) {
            return await this._context.Criminals
                .Where(c => ids.Contains(c.CriminalId))
                .ToListAsync();
        }
        public async Task<List<Criminal>?> GetAll() {
            return await this._context.Criminals
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Crime>?> GetCrimes(Criminal model) {
            return await _context.Crimes
                .Where(c => c.Criminals.Any(cr => cr.CriminalId == model.CriminalId))
                .ToListAsync();
        }

        public async Task<Criminal?> Exists(CriminalViewModel viewModel) {
            if (viewModel == null) throw new Exception("El modelo de vista del criminal es nulo");
            try {
                return await this._context.Criminals
                    .FirstOrDefaultAsync(m => m.Name.Equals(viewModel.Name, StringComparison.CurrentCultureIgnoreCase) &&
                        m.Risk.Name.Equals(viewModel.Risk, StringComparison.CurrentCultureIgnoreCase) &&
                        m.CriminalSince == viewModel.Since
                    );
            } catch (Exception) {
                throw;
            }
        }

        public async Task<CriminalRiskLevel?> GetCriminalRiskLevelAsync(string? riskName) {
            try {
                return await this._context.CriminalRiskLevels.FirstOrDefaultAsync(
                    m => m.Name.Equals(riskName, StringComparison.CurrentCultureIgnoreCase)
                );
            } catch (Exception) {
                throw;
            }
        }

        public async Task Add(Criminal criminal) {
            await this._context.Criminals.AddAsync(criminal);
        }

        public void Update(Criminal criminal) {
            this._context.Criminals.Update(criminal);
        }

        public void Delete(Criminal criminal) {
            this._context.Criminals.Remove(criminal);
        }

        public async Task<int> SaveChanges() {
            return await this._context.SaveChangesAsync();
        }
    }
}
