using System;

namespace RecruitmentWpfApp.Interfaces
{
    public interface IRequestsClose
    {
        event EventHandler<EventArgs> CloseRequest;
    }
}
