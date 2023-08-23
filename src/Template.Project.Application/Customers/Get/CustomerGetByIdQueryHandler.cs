using MediatR;
using Mapster;
using Template.Project.Domain.Interfaces;
using Template.Project.Application.Middlewares.Exceptions;

namespace Template.Project.Application.Customers.Get
{
    public sealed record CustomerGetByIdQueryHandler : IRequestHandler<CustomerGetByIdQuery, CustomerGetByIdResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerGetByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerGetByIdResponse> Handle(CustomerGetByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);

            if (customer is null)
            {
                throw new NotFoundException("Customer was not found!");
            }

            return customer.Adapt<CustomerGetByIdResponse>();
        }
    }
}
