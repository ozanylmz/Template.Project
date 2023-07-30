using MediatR;

namespace Template.Project.Application.Customers.Get
{
    public sealed record CustomerGetByIdQuery(string Id) : IRequest<CustomerGetByIdResponse>;
}
