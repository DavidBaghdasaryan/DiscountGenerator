using AutoMapper;
using DiscountGenerator.DAL.Entity;
using DiscountGenerator.ExportModels;
using DiscountGenerator.Models;

namespace DiscountGenerator.Mapper
{
    public class ProductInfoProfile : Profile
    {
        public ProductInfoProfile()
        {
            CreateMap<ProductInfo, ProductModel>()
                .ForMember(dest => dest.ProdId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<DiscountExport, DiscountModel>()
                    .ForMember(dest => dest.Percent, opt => opt.MapFrom(src => src.Percent))
                    .ForMember(dest => dest.Fix, opt => opt.MapFrom(src => src.Fix));
            CreateMap<DiscountModel, DiscountExport>()
                    .ForMember(dest => dest.Percent, opt => opt.MapFrom(src => src.Percent))
                    .ForMember(dest => dest.Fix, opt => opt.MapFrom(src => src.Fix));
        }
    }
}
