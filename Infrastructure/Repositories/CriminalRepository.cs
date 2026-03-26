using Application.Interfaces.IRepositories;
using Infrastructure.EF_Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CriminalRepository : ICriminalRepository
    {
        private readonly SpidermanContext _context;

        public CriminalRepository(SpidermanContext context) {
            this._context = context;
        }

        public async Task<CriminalEntity?> GetById(int id) {
            return await this._context.Criminals
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CriminalId == id);
        }

        public async Task<List<CriminalEntity>?> GetByIds(List<int> ids) {
            return await this._context.Criminals
                .Where(c => ids.Contains(c.CriminalId))
                .ToListAsync();
        }
        public async Task<List<CriminalEntity>?> GetAll() {
            return await this._context.Criminals
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<CrimeEntity>?> GetCrimes(CriminalEntity model) {
            return await _context.Crimes
                .Where(c => c.Criminals.Any(cr => cr.CriminalId == model.CriminalId))
                .ToListAsync();
        }

        public async Task<CriminalEntity?> Exists(CriminalViewModel viewModel) {
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

        public async Task<CriminalRiskLevelEntity?> GetCriminalRiskLevelAsync(string? riskName) {
            try {
                return await this._context.CriminalRiskLevels.FirstOrDefaultAsync(
                    m => m.Name.Equals(riskName, StringComparison.CurrentCultureIgnoreCase)
                );
            } catch (Exception) {
                throw;
            }
        }

        public async Task Add(CriminalEntity criminal) {
            await this._context.Criminals.AddAsync(criminal);
        }

        public void Update(CriminalEntity criminal) {
            this._context.Criminals.Update(criminal);
        }

        public void Delete(CriminalEntity criminal) {
            this._context.Criminals.Remove(criminal);
        }

        public async Task<int> SaveChanges() {
            return await this._context.SaveChangesAsync();
        }
    }
}
