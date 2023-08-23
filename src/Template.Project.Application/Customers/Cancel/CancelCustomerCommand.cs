using MediatR;

namespace Template.Project.Application.Customers.Cancel
{
    public sealed record CancelCustomerCommand(string Id) : IRequest
    {
       
    }
}
