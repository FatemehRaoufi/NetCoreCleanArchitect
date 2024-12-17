using System.Linq.Expressions;

namespace OnlineStore.Domain.Specifications
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; protected set; }
        public Func<IQueryable<T>, IQueryable<T>>? Includes { get; protected set; }

        public virtual IQueryable<T> Apply(IQueryable<T> query)
        {
            query = query.Where(Criteria);

            if (Includes != null)
            {
                query = Includes(query);
            }

            if (OrderBy != null)
            {
                query = OrderBy(query);
            }

            return query;
        }
    }
}
