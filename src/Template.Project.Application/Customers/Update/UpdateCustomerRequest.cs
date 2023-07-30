using Template.Project.Domain.ValueObjects;

namespace Template.Project.Application.Customers.Update
{
    public record UpdateCustomerRequest(
        string Name,
        string Surname,
        Address? Address);
}
