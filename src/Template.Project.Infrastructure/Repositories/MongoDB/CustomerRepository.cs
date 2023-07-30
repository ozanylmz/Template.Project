using Microsoft.Extensions.Options;
using Template.Project.Domain.AggregateModels.Customer;
using Template.Project.Domain.Interfaces;
using Template.Project.Infrastructure.DbConfigurations.MongoDB;

namespace Template.Project.Infrastructure.Repositories.MongoDB
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IOptions<MongoSettings> options) : base(options)
        {
        }
    }
}
