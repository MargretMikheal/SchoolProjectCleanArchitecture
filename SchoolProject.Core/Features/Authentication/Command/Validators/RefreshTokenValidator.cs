using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Features.Authentication.Command.Models;

namespace SchoolProject.Core.Features.Authentication.Command.Validators
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommond>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Ctor
        public RefreshTokenValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
        }
        #endregion
    }
}
