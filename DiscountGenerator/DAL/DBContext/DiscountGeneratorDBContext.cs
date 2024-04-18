using DiscountGenerator.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DiscountGenerator.DAL.DBContext
{
    public class DiscountGeneratorDBContext : DbContext
    {
        public DiscountGeneratorDBContext(DbContextOptions<DiscountGeneratorDBContext> contextOptions) : base(contextOptions) { }


        public virtual DbSet<ProductInfo>  ProductInfos { get; set; }
    }
}
