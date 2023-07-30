using MediatR;
namespace Template.Project.Application.Customers.Add
{
    public sealed record AddCustomerCommand(string Name, string Surname) : IRequest;
}
