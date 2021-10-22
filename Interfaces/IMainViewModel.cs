using RecruitmentWpfApp.Commands;
using RecruitmentWpfApp.Models;

using System.Collections.ObjectModel;

namespace RecruitmentWpfApp.Interfaces
{
    public interface IMainViewModel
    {
        ParameterizedAsyncRelayCommand<string> LoadDataFromFileCommand { get; }
        ObservableCollection<PersonData> Records { get; }
        ApplicationStateReport Report { get; set; }
    }
}