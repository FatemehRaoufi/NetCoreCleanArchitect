using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Specifications;
using OnlineStore.Domain.Interfaces;

namespace OnlineStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductService(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync() => await _repository.GetAllAsync();

        public async Task<Product?> GetProductByIdAsync(int id) => await _repository.GetByIdAsync(id);
     
        public async Task AddAsync(Product product) => await _repository.AddAsync(product);

        public async Task UpdateAsync(Product product) => await _repository.UpdateAsync(product);

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

       

      

        /* Using Specification Pattern */
        public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, bool isDescending)
        {
            var spec = new ProductByPriceRangeSpecification(minPrice, maxPrice,  isDescending);
            return await _repository.FindAsync(spec);
        }


        //public async Task<IEnumerable<Product>> GetProductsBySpecificationAsync(Specification<Product> specification)
        //{
        //    return await _repository.GetProductsAsync(specification);
        //}
    }
}
