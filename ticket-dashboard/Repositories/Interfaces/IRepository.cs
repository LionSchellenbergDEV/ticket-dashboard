namespace ticket_dashboard.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // CRUD-Methoden hier definieren
        Task<T?> GetByIdAsync(int id);
        // ...
        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
    }
}
