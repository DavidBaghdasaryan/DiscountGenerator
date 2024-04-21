using Quartz.Spi;
using Quartz;

namespace DiscountGenerator.Quartz.JobFactory
{
    public class JobFactory:IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle triggerFiredBundle, IScheduler scheduler)
        {
            return _serviceProvider.GetRequiredService(triggerFiredBundle.JobDetail.JobType) as IJob;
        }
        public void ReturnJob(IJob job) { }
    }
}
