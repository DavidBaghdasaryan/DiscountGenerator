using DiscountGenerator.DAL.DBContext;
using DiscountGenerator.DAL.Entity;

namespace DiscountGenerator.DAL.Repository
{
    public class ProductInfoRepository : BaseRepository<ProductInfo>
    {
        public ProductInfoRepository(DiscountGeneratorDBContext dbContext) : base(dbContext)
        {
           
        }
    }
}
