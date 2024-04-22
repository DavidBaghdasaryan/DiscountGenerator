using AutoMapper;
using DiscountGenerator.Mapper;
using DiscountGenerator.Controllers;
using DiscountGenerator.DAL.Entity;
using DiscountGenerator.Models;
using Microsoft.AspNetCore.Hosting;
using DiscountGenerator.Quartz;
using Quartz;
using Quartz.Spi;
using DiscountGenerator.Quartz.JobFactory;
using Quartz.Impl;

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
        
        public static void ConfigureQuartzService(this IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<GenerateDicountJob>();
            services.AddSingleton(new JobData(Guid.NewGuid(), typeof(GenerateDicountJob), "Generate Dicount Job", "0/10 * 8-21 * * ?", 0, 0));
            services.AddHostedService<QuartzHostedService>();
        }
    }
}
