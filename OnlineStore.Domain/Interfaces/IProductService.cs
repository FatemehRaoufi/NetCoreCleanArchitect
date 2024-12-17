using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product?> GetProductByIdAsync(int id);
       Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, bool isDescending);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
