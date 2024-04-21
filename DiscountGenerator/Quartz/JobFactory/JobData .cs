using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;

public class JobData
{
    public Guid JobId { get; set; }
    public Type JobType { get; }
    public string JobName { get; }
    public string CronExpression { get; }
    public int TimeHour { get; }
    public int TimeMinute { get; }
    public JobData(Guid Id, Type jobType, string jobName, string cronExpression, int timeHour, int timeMinute)
    {
        JobId = Id;
        JobType = jobType;
        JobName = jobName;
        CronExpression = cronExpression;
        TimeHour = timeHour;
        TimeMinute = timeMinute;
    }
}
