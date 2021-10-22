
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.Interfaces
{
    public interface IMessageBoxService
    {
        void ReportError(string title, string message);
        void ReportSuccess(string title, string message);
    }
}
