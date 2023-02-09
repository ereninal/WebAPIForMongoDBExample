using WebAPIForMongoDB.Core.Patterns.Repository.MongoDb;
using WebAPIForMongoDB.Entities.MongoDB;

namespace WebAPIForMongoDB.DataAccess.Base
{
    public interface ICustomerRepository : IRepository<Customer, string>
    {
        Customer GetByName(string name);
    }
}
