using DiscountGenerator.Controllers;

namespace DiscountGenerator.Abstractions
{
    public interface IDiscountManager
    {
        Task<List<ProductModel>> GetInfo();
        Task PostInfo(ProductModel productModel);
        Task PostDiscount();
    }
}
