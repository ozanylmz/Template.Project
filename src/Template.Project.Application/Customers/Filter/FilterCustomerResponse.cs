using Template.Project.Domain.AggregateModels.Customer;
using Template.Project.Domain.Enums;
using Template.Project.Domain.ValueObjects;

namespace Template.Project.Application.Customers.Filter
{
    public record FilterCustomerResponse(
        string Name,
        string Surname,
        CustomerStatus Status,
        Address Address);
}
