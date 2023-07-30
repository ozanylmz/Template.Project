using Template.Project.Domain.ValueObjects;

namespace Template.Project.Application.Customers.Get
{
    public record CustomerGetByIdResponse(
        string Id,
        string Name,
        string Surname,
        Address Address);
}
