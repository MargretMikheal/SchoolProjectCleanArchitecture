using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Features.Emails.Commands.Models;

namespace SchoolProject.Core.Features.Emails.Commands.Validator
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;

        public SendEmailValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Ctor

        #endregion
        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Email).EmailAddress().WithMessage(_localizer[SharedResourcesKeys.IsNotAValidEmailAddress]);
            RuleFor(x => x.Message).NotNull().NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);

        }
        public void ApplyCustomValidationRules()
        {

        }
        #endregion
    }
}

