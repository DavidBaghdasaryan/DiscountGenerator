using AutoMapper;
using DiscountGenerator.Controllers;
using DiscountGenerator.DAL.Entity;

namespace DiscountGenerator.Models
{
    public class ProductInfoProfile:Profile
    {
        public ProductInfoProfile()
        {
            CreateMap<ProductInfo, ProductModel>()
                .ForMember(dest => dest.ProdId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
