using OnlineStore.Domain.Specifications;

namespace OnlineStore.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Specification<T> specification);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
