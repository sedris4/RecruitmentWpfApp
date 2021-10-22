using Dawn;

using FluentValidation.Results;

using RecruitmentWpfApp.Commands;
using RecruitmentWpfApp.Interfaces;
using RecruitmentWpfApp.Models;
using RecruitmentWpfApp.Validation;
using RecruitmentWpfApp.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.ViewModels
{
    public class DataEditorViewModel : ViewModelBase, IRequestsClose
    {
        public PersonData PersonContainer { get; private set; }

        public PersonData OriginalData
        {
            get
            {
                return _originalPersonData;
            }
            set
            {
                _originalPersonData = value;

                PersonContainer = _originalPersonData.Clone();
            }
        }

        public AsyncRelayCommand CommitDataEditCommand { get; }

        private PersonData _originalPersonData;

        private readonly IMessageBoxService _messageBoxService;
        private readonly IPeopleRepository _repository;
        private readonly IApplicationLogger _logger;

        public event EventHandler<EventArgs> CloseRequest;

        /// <summary>
        /// Initializes new instance of <see cref="DataEditorViewModel"/>
        /// </summary>
        /// <param name="originalData"></param>
        public DataEditorViewModel(IPeopleRepository repository, IMessageBoxService messageBoxService, IApplicationLogger logger)
        {
            _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
            _messageBoxService = Guard.Argument(messageBoxService, nameof(messageBoxService)).NotNull().Value;
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;

            CommitDataEditCommand = new AsyncRelayCommand(CommitDataEdit);
        }

        private async Task CommitDataEdit()
        {
            if(Equals(_originalPersonData, PersonContainer))
            {
                _messageBoxService.ReportSuccess("No changes", "No changes to commit.");

                return;
            }

            if (IsInputDataValid(PersonContainer, out string errorMessage))
            {
                if(await _repository.UpdateAsync(_originalPersonData.Id, PersonContainer))
                {
                    _messageBoxService.ReportSuccess("Success", "Data edited successfully.");
                    _logger.Information($"Successfully edited data with id = {_originalPersonData.Id}.");

                    CloseRequest?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    _messageBoxService.ReportError("Error", "Failed to update record.");
                }
            }
            else
            {
                _messageBoxService.ReportError("Error", errorMessage);
                _logger.Warning($"User entered invalid data to container, {errorMessage}");

                PersonContainer = _originalPersonData.Clone();
            }
        }

        private bool IsInputDataValid(PersonData personContainer, out string errorMessage )
        {
            PersonDataValidator personDataValidator = new PersonDataValidator(_originalPersonData);
            ValidationResult validationResult = personDataValidator.Validate(personContainer);

            errorMessage = validationResult.IsValid ? default : validationResult.Errors.First().ErrorMessage;

            return validationResult.IsValid;
        }
    }
}
