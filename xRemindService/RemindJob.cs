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
            Interop.ShowMessageBox(remindString, title);
            await Console.Out.WriteLineAsync(remindString);
        }
    }
}
