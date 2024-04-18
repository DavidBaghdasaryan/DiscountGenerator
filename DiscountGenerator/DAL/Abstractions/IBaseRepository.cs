using System.Linq.Expressions;

namespace DiscountGenerator.DAL.Abstractions
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params string[] includes);
        void Add(T entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
