using Microsoft.Extensions.Options;
using WebAPIForMongoDB.Core.Patterns.Repository.MongoDb;
using WebAPIForMongoDB.Core.Settings.MongoDB;
using WebAPIForMongoDB.DataAccess.Base;
using WebAPIForMongoDB.Entities.MongoDB;

namespace WebAPIForMongoDB.DataAccess.Repository
{
    public class CustomerRepository : MongoDbRepositoryBase<Customer>, ICustomerRepository
    {
        
        public CustomerRepository(IOptions<MongoDbSettings> options) : base(options)
        {

        }

        public Customer GetByName(string name)
        {

            return new Customer { };
        }
    }
}
