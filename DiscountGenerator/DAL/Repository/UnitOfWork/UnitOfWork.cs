using DiscountGenerator.DAL.Abstractions;
using DiscountGenerator.DAL.Abstractions.UnitOfWork;
using DiscountGenerator.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DiscountGenerator.DAL.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        IProductInfoRepository productInfoRepository;
        DiscountGeneratorDBContext _dbContext;
        protected IConfiguration _configuration;

        public UnitOfWork(DiscountGeneratorDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public IProductInfoRepository ProductInfoRepository
        {
            get { return productInfoRepository = productInfoRepository ?? new ProductInfoRepository(_dbContext); }
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        public string GetValuesFromApps(string section) 
        {
            var valu=_configuration.GetSection("AppSettings:"+ section).Value;
            return valu;
        }
    }
}
