using MongoDB.Bson;
using WebAPIForMongoDB.Core.Patterns.Repository.MongoDb;
using WebAPIForMongoDB.Entities.MongoDB;

namespace WebAPIForMongoDB.DataAccess.Base
{
    public interface ICustomerRepository : IRepository<Customer, ObjectId>
    {
        Customer GetByName(string name);
    }
}
