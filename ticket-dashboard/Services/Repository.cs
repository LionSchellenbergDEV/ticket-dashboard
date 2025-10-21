using Microsoft.EntityFrameworkCore;
using ticket_dashboard.Repositories.Interfaces;
namespace ticket_dashboard.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        // DbContext im Konstruktor anfordern
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Beispielimplementierung
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        // ...
    }
}
