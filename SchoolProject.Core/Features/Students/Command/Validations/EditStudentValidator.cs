using FluentValidation;
using SchoolProject.Core.Features.Students.Command.Models;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Students.Command.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentService _studentService;

        public EditStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.NameAr).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.DepartmentID).NotEmpty().WithMessage("Department is required");
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Phone).Matches(@"^\d{11}$").WithMessage("Phone number must be 11 digits");
            RuleFor(x => x.NameAr).MustAsync(async (name, cancellation) =>
            {
                return await Task.FromResult(name.Length > 3);
            }).WithMessage("Name must be more than 3 characters");

            RuleFor(x => x.NameAr).MustAsync(async (model, name, cancellation) =>
            {
                return !await _studentService.IsNameExistAsyncExcludeSelf(name, model.Id);
            }).WithMessage("Exists");
        }
    }
}
