namespace ticket_dashboard.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // CRUD-Methoden hier definieren
        Task<T?> GetByIdAsync(int id);
        // ...
    }
}
