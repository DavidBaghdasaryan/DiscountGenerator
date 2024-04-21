using DiscountGenerator.ExportModels;
using DiscountGenerator.Models;

namespace DiscountGenerator.Abstractions
{
    public interface IDiscountManager
    {
        Task<List<ProductModel>> GetInfo();
        Task PostInfo(ProductModel productModel);
        Task PostDiscount(DiscountModel discount);
    }
}
