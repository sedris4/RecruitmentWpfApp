using CsvHelper;

using Dawn;

using FluentValidation.Results;

using RecruitmentWpfApp.Commands;
using RecruitmentWpfApp.Enums;
using RecruitmentWpfApp.Interfaces;
using RecruitmentWpfApp.Models;
using RecruitmentWpfApp.Services;
using RecruitmentWpfApp.Validation;
using RecruitmentWpfApp.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RecruitmentWpfApp.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        //===============================================================
        private ApplicationStateReport report;
        /// <summary>
        /// Describes current application state, informs about operations results
        /// </summary>
        public ApplicationStateReport Report
        {
            get { return report; }
            set
            {
                SetProperty(ref report, value);
                StartChangeToIdleTimer();
            }
        }
        //=============================================================== 

        /// <summary>
        /// Loaded records collection
        /// </summary>
        public ObservableCollection<PersonData> Records => _repository.Data;
        /// <summary>
        /// Load data from file command
        /// </summary>
        public ParameterizedAsyncRelayCommand<string> LoadDataFromFileCommand { get; }

        private readonly IPersonDataLoadersBank _personDataLoadersBank;
        private readonly IPeopleRepository _repository;
        private readonly IApplicationLogger _logger;

        private Task _timerTask;
        private CancellationTokenSource _timerCts = new CancellationTokenSource();

        /// <summary>
        /// Initializes new instance of <see cref="MainViewModel"/>
        /// </summary>
        /// <param name="personDataLoadersBank"></param>
        public MainViewModel(IPersonDataLoadersBank personDataLoadersBank, IPeopleRepository repository, IApplicationLogger logger)
        {
            _personDataLoadersBank = Guard.Argument(personDataLoadersBank, nameof(personDataLoadersBank)).NotNull().Value;
            _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;

            Report = ApplicationStateReport.Idle;
            LoadDataFromFileCommand = new ParameterizedAsyncRelayCommand<string>(LoadDataFromFile, CanLoadDataFromFile);
        }

        private bool CanLoadDataFromFile(object obj)
        {
            if (obj is not string path)
            {
                Report = new ApplicationStateReport(ApplicationState.Error, "Path invalid.");

                return false;
            }

            ValidationResult validationResult = FilepathValidator.Default.Validate(path);

            if (validationResult.IsValid is false)
            {
                Report = new ApplicationStateReport(ApplicationState.Error, validationResult.Errors.First().ErrorMessage);

                return false;
            }

            return true;
        }

        private async Task LoadDataFromFile(string filePath)
        {

            try
            {
                IPersonDataLoader loader = _personDataLoadersBank.Get(fileExtension: Path.GetExtension(filePath));

                _ = await _repository.InsertAsync(loader.LoadAsync(filePath));

                Report = new ApplicationStateReport(ApplicationState.Success, "Data loaded successfully.");
            }
            catch (ValidationException ex)
            {
                Report = new ApplicationStateReport(ApplicationState.Error, "File format invalid.");

                _logger.Error("Input csv file format invalid.", ex);
            }
            catch (Exception ex)
            {
                Report = new ApplicationStateReport(ApplicationState.Error, ex.Message);

                _logger.Error("Failed to load people data from file.", ex);
            }
        }

        private void StartChangeToIdleTimer()
        {
            _timerCts.Cancel();
            _timerCts = new CancellationTokenSource();

            _timerTask = Task.Delay(1500).ContinueWith((t) => SetProperty(ref report, ApplicationStateReport.Idle, nameof(Report)),
                _timerCts.Token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
        }
    }
}
