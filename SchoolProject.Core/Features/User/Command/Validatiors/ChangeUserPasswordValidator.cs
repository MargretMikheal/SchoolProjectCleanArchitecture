using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Features.User.Command.Models;

namespace SchoolProject.Core.Features.User.Command.Validatiors
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructor
        public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Methods
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.NewPassword).Equal(x => x.ConfirmedNewPassword).WithMessage(_localizer[SharedResourcesKeys.PasswordsDoNotMatch]);
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.ConfirmedNewPassword).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.UserId).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        #endregion
    }
}
