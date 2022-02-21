using Microsoft.Toolkit.Uwp.Notifications;
using Quartz;
using System;
using System.Threading.Tasks;

namespace xRemindService
{
    public class RemindJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var data = context.JobDetail.JobDataMap;
            var remindString = data.GetString(Constants.RemindString);
            var title = data.GetString(Constants.Title);
            new ToastContentBuilder()
                .AddText(title)
                .AddText(remindString).Show();
            await Console.Out.WriteLineAsync(remindString);
        }
    }
}
