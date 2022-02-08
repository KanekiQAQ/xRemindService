using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using System.Threading;
using System.Threading.Tasks;

namespace xRemindService
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;

        public Worker(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Interop.Hide();

            var jobs = _configuration.GetSection(Constants.CronJob).GetChildren();
            foreach (var cronJob in jobs)
            {
                var jobName = cronJob.Key;
                var triggerName = $"{jobName}Trigger";
                var groupName = $"{jobName}Group";
                var title = _configuration.GetSection($"{Constants.CronJob}:{jobName}:{Constants.Title}").Value.checkValue(jobName,Constants.Title);
                var cronScheduler = _configuration.GetSection($"{Constants.CronJob}:{jobName}:{Constants.CronScheduler}").Value.checkValue(jobName, Constants.CronScheduler);
                var remindString = _configuration.GetSection($"{Constants.CronJob}:{jobName}:{Constants.RemindString}").Value.checkValue(jobName, Constants.RemindString);

                var factory = new StdSchedulerFactory();
                var scheduler = await factory.GetScheduler();

                await scheduler.Start();

                var job = JobBuilder.Create<RemindJob>()
                    .WithIdentity(jobName, groupName)
                    .Build();

                job.JobDataMap.Put(Constants.RemindString, remindString);
                job.JobDataMap.Put(Constants.Title, title);

                var trigger = TriggerBuilder.Create()
                    .WithIdentity(triggerName, groupName)
                    .StartNow()
                    .WithCronSchedule(cronScheduler)
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}
