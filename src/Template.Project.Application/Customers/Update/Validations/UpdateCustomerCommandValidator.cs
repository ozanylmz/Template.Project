using FluentValidation;

namespace Template.Project.Application.Customers.Update.Validations
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(row => row.Request.Name)
                .NotNull().WithMessage("Name can not be null")
                .NotEmpty().WithMessage("Name can not be empty");

            RuleFor(c => c.Request.Surname)
                .NotNull().WithMessage("Surname can not be null")
                .NotEmpty().WithMessage("Surname can not be empty");
        }
    }
}
