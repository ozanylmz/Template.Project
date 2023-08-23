using Mapster;
using MediatR;
using Template.Project.Application.Extensions;
using Template.Project.Application.Middlewares.Exceptions;
using Template.Project.Domain.Interfaces;

namespace Template.Project.Application.Customers.Filter
{
    public sealed record CustomerFilterQueryHandler : IRequestHandler<CustomerFilterQuery, List<FilterCustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerFilterQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<List<FilterCustomerResponse>> Handle(CustomerFilterQuery request, CancellationToken cancellationToken)
        {
            var predicate = await FilterHelper.GetFilterPredicate(request.QueryFilter);
            var customer = await _customerRepository.Get(predicate);

            if (customer is null)
            {
                throw new NotFoundException("Customer was not found!");
            }

            return customer.Adapt<List<FilterCustomerResponse>>();
        }
    }
}
