using DiscountGenerator.Abstractions;
using Quartz;

namespace DiscountGenerator.Quartz
{
    [DisallowConcurrentExecution]
    public class GenerateDicountJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public GenerateDicountJob(IServiceScopeFactory serviceScopeFactory)
        {
                _serviceScopeFactory = serviceScopeFactory; 
        }
        public Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                IDiscountManager orderManager = scope.ServiceProvider.GetService<IDiscountManager>();

            }
            return Task.CompletedTask;
        }
    }
}
