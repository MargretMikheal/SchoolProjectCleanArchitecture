using FluentValidation;
using SchoolProject.Core.Features.Students.Command.Models;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Students.Command.Validations
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion
        #region Ctor
        public AddStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.DepartmentID).NotEmpty().WithMessage("Department is required");
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Phone).Matches(@"^\d{11}$").WithMessage("Phone number must be 11 digits");
            RuleFor(x => x.Name).MustAsync(async (name, cancellation) =>
            {
                return await Task.FromResult(name.Length > 3);
            }).WithMessage("Name must be more than 3 characters");
            RuleFor(x => x.Name).MustAsync(async (name, cancellation) =>
            {
                return !await _studentService.IsNameExistAsync(name);
            }).WithMessage("Exists");
        }
        #endregion
    }
}
