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
        public static void ConfigureQuartzService(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<CalculateOzonToOrderJob>();
            //services.AddSingleton(new JobData(Guid.NewGuid(), typeof(CalculateOzonToOrderJob), "Calculate Ozon to Order", "0 0/30 1-23 ? * *", 0, 0));
    
        }
    }
}
