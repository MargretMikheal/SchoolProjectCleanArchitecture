using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Features.Authentication.Query.Models;

namespace SchoolProject.Core.Features.Authentication.Query.Validator
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Ctor
        public ConfirmEmailValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(x => x.userId).NotEmpty().NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.code).NotEmpty().NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
        }
        #endregion
    }
}
