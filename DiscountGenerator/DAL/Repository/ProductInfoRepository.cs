using DiscountGenerator.DAL.Abstractions;
using DiscountGenerator.DAL.DBContext;
using DiscountGenerator.DAL.Entity;

namespace DiscountGenerator.DAL.Repository
{
    public class ProductInfoRepository : BaseRepository<ProductInfo>,IProductInfoRepository
    {
        public ProductInfoRepository(DiscountGeneratorDBContext dbContext) : base(dbContext)
        {
           _dbContext = dbContext;
        }
        

        public  List<ProductInfo > GetAllProducts()
        {
            return  _dbContext.ProductInfos.ToList();    
        }
    }
}
