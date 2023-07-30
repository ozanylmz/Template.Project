using Template.Project.Domain.SeedWork;
using Template.Project.Domain.ValueObjects;

namespace Template.Project.Domain.AggregateModels.Customer
{
    public class Customer : MongoBaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public Address? Address { get; private set; }

        public Customer(string name, string surname, Address? address)
        {
            Name = name;
            Surname = surname;
            Address = address;
        }

    }
}
