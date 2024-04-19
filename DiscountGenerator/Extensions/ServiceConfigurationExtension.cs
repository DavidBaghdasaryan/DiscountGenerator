using AutoMapper;
using DiscountGenerator.Mapper;
using DiscountGenerator.Controllers;
using DiscountGenerator.DAL.Entity;
using DiscountGenerator.Models;
using Microsoft.AspNetCore.Hosting;

namespace DiscountGenerator.Extensions
{
    public static class ServiceConfigurationExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductInfoProfile());
                
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

    }
}
