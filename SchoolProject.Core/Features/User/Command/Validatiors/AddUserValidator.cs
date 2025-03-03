using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Features.User.Command.Models;

namespace SchoolProject.Core.Features.User.Command.Validatiors
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructor
        public AddUserValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Methods
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage(_localizer[SharedResourcesKeys.PasswordsDoNotMatch]);
            RuleFor(x => x.Email).EmailAddress().WithMessage(_localizer[SharedResourcesKeys.Invalid]);
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Email).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Password).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.UserName).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        #endregion
    }
}
