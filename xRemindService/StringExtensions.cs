using System.Diagnostics;

namespace xRemindService
{
    public static class StringExtensions
    {
        public static string checkValue(this string str, string jobName, string type)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                Interop.ShowMessageBox($"{jobName}没有配置参数{type}", "提示");
                Process.GetCurrentProcess().Kill();
            }
            return str;
        }
    }
}
