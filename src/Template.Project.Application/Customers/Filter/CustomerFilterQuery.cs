using MediatR;

namespace Template.Project.Application.Customers.Filter
{
    public sealed record CustomerFilterQuery(string QueryFilter) : IRequest<List<FilterCustomerResponse>>;
}
