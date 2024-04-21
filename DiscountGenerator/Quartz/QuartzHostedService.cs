using DiscountGenerator.Quartz.JobFactory;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Threading;
using System.Threading.Tasks;

public class QuartzHostedService : IHostedService
{

    private readonly ISchedulerFactory schedulerFactory;
    private readonly IJobFactory jobFactory;
    private readonly IEnumerable<JobData> jobSchedules;
  
    public QuartzHostedService(ISchedulerFactory schedulerFactory, IEnumerable<JobData> jobMetadata, IJobFactory jobFactory)
    {
        this.schedulerFactory = schedulerFactory;
        this.jobSchedules = jobMetadata;
        this.jobFactory = jobFactory;
    }

    public IScheduler Scheduler { get; set; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Scheduler = await schedulerFactory.GetScheduler();
        Scheduler.JobFactory = jobFactory;

        foreach (var jobSchedule in this.jobSchedules)
        {
            var job = CreateJob(jobSchedule);
            var trigger = CreateTrigger(jobSchedule);

            await this.Scheduler.ScheduleJob(job, trigger, cancellationToken);
        }

        await Scheduler.Start(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Scheduler?.Shutdown(cancellationToken);
    }

    private ITrigger CreateTrigger(JobData jobData)
    {
        if (!string.IsNullOrEmpty(jobData.CronExpression))
        {
            return TriggerBuilder.Create()
            .WithIdentity(jobData.JobId.ToString())
            .WithCronSchedule(jobData.CronExpression)
            .WithDescription($"{jobData.JobName}")
            .Build();
        }

        return TriggerBuilder.Create()
          .WithDailyTimeIntervalSchedule(s => s
          .OnEveryDay()
          .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(jobData.TimeHour, jobData.TimeMinute)).InTimeZone(TimeZoneInfo.Utc)
          .EndingDailyAfterCount(1)).Build();
    }

    private IJobDetail CreateJob(JobData jobMetadata)
    {
        return JobBuilder
        .Create(jobMetadata.JobType)
        .WithIdentity(jobMetadata.JobId.ToString())
        .WithDescription($"{jobMetadata.JobName}")
        .Build();
    }
}

