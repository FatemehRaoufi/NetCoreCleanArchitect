using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Interfaces
{
    public interface IProductRepository
    {
            Task<IEnumerable<Product>> GetAllAsync();
            Task<Product?> GetByIdAsync(int id);
            Task AddAsync(Product product);
            Task UpdateAsync(Product product);
            Task DeleteAsync(int id);
       
    }

}

