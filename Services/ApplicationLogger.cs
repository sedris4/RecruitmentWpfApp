using RecruitmentWpfApp.Interfaces;

using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.Services
{
    /// <summary>
    /// Log messages logger
    /// </summary>
    public class ApplicationLogger : IApplicationLogger
    {
        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public void Information(string message)
        {
            Log.Information(message);
        }

        public void Warning(string message)
        {
            Log.Warning(message);
        }
    }
}
