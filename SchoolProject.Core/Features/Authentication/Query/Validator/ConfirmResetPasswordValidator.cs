﻿using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Features.Authentication.Query.Models;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Authentication.Command.Validators
{
    public class ConfirmResetPasswordValidator : AbstractValidator<ConfirmResetPasswordQuery>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IApplicationUserService _applicationUserService;
        #endregion

        #region Constructor
        public ConfirmResetPasswordValidator(IStringLocalizer<SharedResources> localizer, IApplicationUserService applicationUserService)
        {
            _localizer = localizer;
            _applicationUserService = applicationUserService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Methods
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage(_localizer[SharedResourcesKeys.InvalidEmail]);

            RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
            {
                var user = await _applicationUserService.FindByEmailAsync(email);
                return user != null;
            }).WithMessage(_localizer[SharedResourcesKeys.UserIsNotFound]);
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        #endregion
    }
}
