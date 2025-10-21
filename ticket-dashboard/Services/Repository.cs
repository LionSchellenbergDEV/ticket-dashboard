using Microsoft.EntityFrameworkCore;
using ticket_dashboard.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ticket_dashboard.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- Vorhandene Methoden ---
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        // --- NEUE HINZUZUFÜGENDE METHODEN ---

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync(); // Wichtig: Änderungen speichern!
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Methode für Batch-Inserts (optional, aber nützlich für Mail-Import)
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        // Beenden Sie die Transaktion explizit, falls Sie IUnitOfWork nicht verwenden
        // ...
    }
}