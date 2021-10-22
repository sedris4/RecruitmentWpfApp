using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace RecruitmentWpfApp.Validation
{
    /// <summary>
    /// Validates filepath provided by user
    /// </summary>
    public class FilepathValidator : AbstractValidator<string>
    {
        public static IValidator<string> Default { get; } = new FilepathValidator();

        private const string FilePathEmptyErrorMessage = "Selected path cannot be empty.";
        private const string FileNotExistsErrorMessage = "File on selected path does not exist.";
        private const string ExtensionInvalidErrorMessage = "File on selected path has invalid extension.";
        
        /// <summary>
        /// Initializes new instance of <see cref="FilepathValidator"/>
        /// </summary>
        public FilepathValidator()
        {
            RuleFor(str => str).Must(str => !string.IsNullOrWhiteSpace(str)).WithMessage(FilePathEmptyErrorMessage);
            RuleFor(str => str).Must(str => File.Exists(str)).WithMessage(FileNotExistsErrorMessage);
            RuleFor(str => str).Must(str => !string.IsNullOrWhiteSpace(Path.GetExtension(str))).WithMessage(ExtensionInvalidErrorMessage);
        }
    }
}
