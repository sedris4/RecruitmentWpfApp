using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.Interfaces
{
    /// <summary>
    /// Interface for in-application info/warning/error logger
    /// </summary>
    public interface IApplicationLogger
    {
        /// <summary>
        /// Logs information message
        /// </summary>
        /// <param name="message"></param>
        void Information(string message);
        /// <summary>
        /// Logs warning message
        /// </summary>
        /// <param name="message"></param>
        void Warning(string message);
        /// <summary>
        /// Logs error message
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        /// <summary>
        /// Logs error message and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Error(string message, Exception exception);
    }
}
