using MediatR;
using Template.Project.Application.Middlewares.Exceptions;
using Template.Project.Domain.AggregateModels.Customer;
using Template.Project.Domain.Interfaces;

namespace Template.Project.Application.Customers.Patch
{
    public sealed record PatchCustomerCommandHandler : IRequestHandler<PatchCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        public PatchCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Unit> Handle(PatchCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(command.Id);

            if (customer is null)
            {
                throw new NotFoundException("Customer was not found!");
            }

            var patchCustomerRequest = new PatchCustomerRequest();
            command.Request.ApplyTo(patchCustomerRequest);

            try
            {
                var newCustomer = new Customer(
                    patchCustomerRequest.Name ?? customer.Name,
                    patchCustomerRequest.Surname ?? customer.Surname,
                    customer.Status,
                    patchCustomerRequest.Address ?? customer.Address);
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
