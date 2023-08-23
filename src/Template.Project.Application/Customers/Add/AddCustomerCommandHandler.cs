using MediatR;
using Template.Project.Domain.AggregateModels.Customer;
using Template.Project.Domain.Enums;
using Template.Project.Domain.Interfaces;

namespace Template.Project.Application.Customers.Add
{
    public sealed record AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        public AddCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Unit> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
        {
            try
            {
               var result = await _customerRepository.AddAsync(new Customer(
                    command.Name,
                    command.Surname,
                    CustomerStatus.Active,
                    null));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error : Customer could not be saved! Message : {ex.InnerException}");
            }

            return Unit.Value;
        }
    }
}
