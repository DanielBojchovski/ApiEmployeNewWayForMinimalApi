using ApiEmployee_NewWayForMinimalApi_.Models;
using FluentValidation;

namespace ApiEmployee_NewWayForMinimalApi_.Validation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Salary).NotEmpty().InclusiveBetween(18000, 617500);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.Now);
            RuleFor(x => x.Department).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}
