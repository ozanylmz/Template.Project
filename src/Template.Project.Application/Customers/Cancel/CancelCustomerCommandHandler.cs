using MediatR;
using Template.Project.Application.Middlewares.Exceptions;
using Template.Project.Domain.AggregateModels.Customer;
using Template.Project.Domain.Enums;
using Template.Project.Domain.Interfaces;

namespace Template.Project.Application.Customers.Cancel
{
    public sealed record CancelCustomerCommandHandler : IRequestHandler<CancelCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        public CancelCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Unit> Handle(CancelCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(command.Id);

            if (customer is null)
            {
                throw new NotFoundException("Customer was not found!");
            }

            try
            {
                var newCustomer = new Customer(
                    customer.Name,
                    customer.Surname,
                    CustomerStatus.Passive,
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
