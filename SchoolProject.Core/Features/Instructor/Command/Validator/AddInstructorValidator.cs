using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Features.Instructor.Command.Models;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Instructor.Command.Validators
{
    public class AddInstructorValidator : AbstractValidator<AddInstructorCommand>
    {
        private readonly IInstructorService _instructorService;
        private readonly IDepartmentService _departmentService;

        public AddInstructorValidator(IStringLocalizer<SharedResources> localizer, IInstructorService instructorService, IDepartmentService departmentService)
        {
            _instructorService = instructorService;
            _departmentService = departmentService;

            RuleFor(x => x.NameAr).NotEmpty().WithMessage(localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.NameEn).NotEmpty().WithMessage(localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Position).NotEmpty().WithMessage(localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Address).NotEmpty().WithMessage(localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Salary).NotEmpty().WithMessage(localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.NameAr).MustAsync(async (name, cancellation) =>
            {
                var exists = await _instructorService.IsNameExistAsync(name);
                return !exists;
            }).WithMessage(localizer[SharedResourcesKeys.Exists]);

            RuleFor(x => x.NameEn).MustAsync(async (name, cancellation) =>
            {
                var exists = await _instructorService.IsNameExistAsync(name);
                return !exists;
            }).WithMessage(localizer[SharedResourcesKeys.Exists]);

            RuleFor(x => x.SupervisorId)
                .MustAsync(async (id, cancellation) => await SupervisorExists(id))
                .WithMessage(localizer["Supervisor not found"]);

            RuleFor(x => x.DID)
                .MustAsync(async (id, cancellation) => await DepartmentExists(id))
                .WithMessage(localizer["Department not found"]);
        }

        private async Task<bool> SupervisorExists(int? supervisorId)
        {
            if (supervisorId.HasValue)
            {
                return await _instructorService.SupervisorExistsAsync(supervisorId.Value);
            }
            return true;
        }

        private async Task<bool> DepartmentExists(int? departmentId)
        {
            if (departmentId.HasValue)
            {
                return await _departmentService.IsExistAsync(departmentId.Value);
            }
            return true;
        }
    }
}
