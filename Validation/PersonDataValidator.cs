using FluentValidation;

using RecruitmentWpfApp.Interfaces;
using RecruitmentWpfApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.Validation
{
    public class PersonDataValidator : AbstractValidator<PersonData>
    {
        private readonly PersonData _comparableInstance;

        public PersonDataValidator(PersonData comparableInstance)
        {
            _comparableInstance = comparableInstance;

            RuleFor(pd => pd.Id).Cascade(CascadeMode.Stop).Equal(_comparableInstance.Id);
            RuleFor(pd => pd.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(pd => pd.LastName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Surname cannot be empty.");
            RuleFor(pd => pd.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Email cannot be empty.");
            RuleFor(pd => pd.Email).Cascade(CascadeMode.Stop).EmailAddress().WithMessage("Email format invalid.");
            RuleFor(pd => pd.TelephoneNumber).Cascade(CascadeMode.Stop).MinimumLength(3).MaximumLength(15).WithMessage("Phone number length invalid");
            RuleFor(pd => pd.TelephoneNumber).Cascade(CascadeMode.Stop).Custom(IsPhoneNumber);
        }

        private void IsPhoneNumber(string text, ValidationContext<PersonData> context)
        {
            if(Regex.IsMatch(text, @"^[0-9]{3,15}$") is false)
            {
                context.AddFailure(new FluentValidation.Results.ValidationFailure(context.DisplayName, "Phone number format invalid.", text));
            }
        }
    }
}
