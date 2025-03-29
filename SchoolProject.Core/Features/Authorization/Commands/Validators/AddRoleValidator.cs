using FluentValidation;

using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Ctor
        public AddRoleValidator(IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService)
        {
            _localizer = localizer;
            ApplyCustomValidationRules();
            ApplyValidationRules();
            _authorizationService = authorizationService;
        }
        #endregion
        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.RoleName).MustAsync(async (name, cancellation) =>
            {
                return !await _authorizationService.IsRoleExistAsync(name);
            }).WithMessage(_localizer[SharedResourcesKeys.Exists]);
        }
        #endregion
    }
}
