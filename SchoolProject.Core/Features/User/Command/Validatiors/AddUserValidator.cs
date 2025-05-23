﻿using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Features.User.Command.Models;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.User.Command.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IApplicationUserService _applicationUserService;
        #endregion

        #region Constructor
        public AddUserValidator(IStringLocalizer<SharedResources> localizer, IApplicationUserService applicationUserService)
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
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage(_localizer[SharedResourcesKeys.PasswordsDoNotMatch]);
            RuleFor(x => x.Email).EmailAddress().WithMessage(_localizer[SharedResourcesKeys.InvalidEmail]);

            RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
            {
                var user = await _applicationUserService.FindByEmailAsync(email);
                return user == null;
            }).WithMessage(_localizer[SharedResourcesKeys.EmailAlreadyTaken]);

            RuleFor(x => x.UserName).MustAsync(async (userName, cancellation) =>
            {
                var user = await _applicationUserService.FindByNameAsync(userName);
                return user == null;
            }).WithMessage(_localizer[SharedResourcesKeys.UserNameAlreadyTaken]);
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
