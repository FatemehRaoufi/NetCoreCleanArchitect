using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Specifications;
using OnlineStore.Infrastructure.Persistence;
using OnlineStore.Domain.Interfaces;
using System.Linq;

namespace OnlineStore.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Product>> GetAllAsync()
        //{
        //    return await _context.Products.ToListAsync();
        //}
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        //--------------
        //public async Task<Product?> GetByIdAsync(int id)
        //{
        //    return await _context.Products.FindAsync(id);
        //}
        /// <summary>
        /// with specification pattern
        /// FindAsync
        /// <param name="specification"></param>
        /// <returns>query.ToListAsync()</returns>
        /// </summary>

        public async Task<IEnumerable<Product>> FindAsync(Specification<Product> specification)
        {
            var query = _context.Products.AsQueryable();
            query = specification.Apply(query);
            return await query.ToListAsync();
        }
       
        public Task<Product?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        //-------------------------------------------------
         async Task<IEnumerable<T>> IGenericRepository<T>.GetAllAsync()
        {
            return (IEnumerable<T>)await _context.Products.ToListAsync();
        }

        async Task<T?> IGenericRepository<T>.GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

       public async Task<IEnumerable<T>> FindAsync(Specification<T> specification)
        {
            // Apply the specification to the query
            var query = specification.Apply(_context.Set<T>());

            // Execute the query and return the results
            return await query.ToListAsync();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
