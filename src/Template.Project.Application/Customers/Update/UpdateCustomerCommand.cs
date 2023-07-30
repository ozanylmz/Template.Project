using MediatR;
using Template.Project.Application.Middlewares.Interfaces;

namespace Template.Project.Application.Customers.Update
{
    public sealed record UpdateCustomerCommand(string Id, UpdateCustomerRequest Request) : IRequest, ICacheableMediatr
    {
        public string CacheKey => $"CustomerId_{Id}";
    }
}
