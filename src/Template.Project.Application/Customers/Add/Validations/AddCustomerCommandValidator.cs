using FluentValidation;

namespace Template.Project.Application.Customers.Add.Validations
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(row => row.Name)
                .NotNull().WithMessage("Name can not be null")
                .NotEmpty().WithMessage("Name can not be empty");

            RuleFor(c => c.Surname)
                .NotNull().WithMessage("Surname can not be null")
                .NotEmpty().WithMessage("Surname can not be empty");
        }
    }
}
