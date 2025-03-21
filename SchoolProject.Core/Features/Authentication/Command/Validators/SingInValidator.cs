using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Features.Authentication.Command.Models;

namespace SchoolProject.Core.Features.Authentication.Command.Validators
{
    public class SingInValidator : AbstractValidator<SignInCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Ctor
        public SingInValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Password).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
        }
        #endregion
    }
}