using System;
using ZLog;

namespace ZLogConsole
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            var perfTracker = new PerformanceTracker("ZLog", "mday", "Mike Day", "laptop", "Logging", "Application");
            var diagLogger = new DiagnosticLogger();
            var usageLogger = new UsageLogger();
            diagLogger.WriteLog(GetZLogDetail("Writing Diagnostics", null));
            usageLogger.WriteLog(GetZLogDetail("Usage Logger", null));

            try
            {
                var i = 1;
                System.Diagnostics.Trace.Write(i/0);
            }
            catch (Exception ex)
            {
                var logDetail = GetZLogDetail(ex.Message, ex);
                new ErrorLogger().WriteLog(logDetail);
            }

            perfTracker.Stop();
        }

        private static LogDetail GetZLogDetail(string message, Exception ex)
        {
            return new LogDetail
            {
                Product = "ZLogger",
                Location = "ZLoggerConsole", // this application
                Layer = "Job", // unattended executable invoked somehow
                UserName = Environment.UserName,
                Hostname = Environment.MachineName,
                Message = message,
                Timestamp = DateTime.Now,
                Exception = ex
            };
        }
    }

}
