using FluentValidation;
using MediatR;
using Template.Project.Application.Customers.Update.Validations;
using Template.Project.Application.Middlewares.Exceptions;
using Template.Project.Domain.AggregateModels.Customer;
using Template.Project.Domain.Interfaces;

namespace Template.Project.Application.Customers.Update
{
    public sealed record UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Unit> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateCustomerCommandValidator();
            var validatorResult = validator.Validate(command);

            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult.Errors);

            var customer = await _customerRepository.GetByIdAsync(command.Id);

            if (customer is null)
            {
                throw new NotFoundException("Customer was not found!");
            }

            try
            {
                var newCustomer = new Customer(
                    command.Request.Name,
                    command.Request.Surname,
                    customer.Status,
                    null);
                newCustomer.SetId(customer.Id);
                newCustomer.SetCreatedDate(customer.CreatedDate);
                newCustomer.SetUpdateDate();

                var result = await _customerRepository.UpdateAsync(customer.Id, newCustomer);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Customer could not be updated! Message : {ex.InnerException}");
            }

            return Unit.Value;
        }
    }
}
