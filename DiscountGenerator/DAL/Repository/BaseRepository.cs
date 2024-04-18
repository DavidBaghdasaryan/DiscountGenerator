using DiscountGenerator.DAL.Abstractions;
using DiscountGenerator.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DiscountGenerator.DAL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DiscountGeneratorDBContext _dbContext;
        public BaseRepository(DiscountGeneratorDBContext dbContext)
        {
            _dbContext = dbContext;

        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            for (int i = 0; i < includes.Length; i++)
            {
                set = set.Include(includes[i]);
            }

            if (predicate == null)
            {
                return set;
            }

            return set.AsNoTracking().Where(predicate);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return  _dbContext.SaveChangesAsync();
        }
    }
}
