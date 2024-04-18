using DiscountGenerator.DAL.Entity;

namespace DiscountGenerator.DAL.Abstractions
{
    public interface IProductInfoRepository:IBaseRepository<ProductInfo>
    {
        List<ProductInfo> GetAllProducts();
    }
}
