namespace Assignment3_Backend.Models
{
    public interface IRepository
    {
        Task<bool> SaveChangesAsync();
        Task<T[]> GetAllAsync<T>() where T : class; 
        void Add<T>(T entity) where T : class;
    }
}
