using Template.Project.Domain.ValueObjects;

namespace Template.Project.Application.Customers.Patch
{
    public class PatchCustomerRequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Address? Address { get; set; }
    }

}
