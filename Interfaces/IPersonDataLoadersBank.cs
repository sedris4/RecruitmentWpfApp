using RecruitmentWpfApp.Services;

namespace RecruitmentWpfApp.Interfaces
{
    /// <summary>
    /// Container for available data file loaders
    /// </summary>
    public interface IPersonDataLoadersBank
    {
        IPersonDataLoader Get(string fileExtension);
    }
}