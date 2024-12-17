using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Specifications;
using System.Linq.Expressions;

namespace OnlineStore.Domain.Specifications
{
    public class ProductByPriceRangeSpecification : Specification<Product>
    {
        private readonly decimal _minPrice;
        private readonly decimal _maxPrice;
        private readonly bool _isDescending;

        public ProductByPriceRangeSpecification(decimal minPrice, decimal maxPrice, bool isDescending)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
            _isDescending = isDescending;
            // Define the criteria for price range filtering
            Criteria = product => product.Price >= _minPrice && product.Price <= _maxPrice;

            // Define the ordering by price (ascending by default)
            OrderBy = products => products.OrderBy(p => p.Price);


            // Apply ascending or descending ordering based on the _isDescending flag
            OrderBy = products => _isDescending ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price);
        }
                
        public override Expression<Func<Product, bool>> Criteria { get; }
    }
}
