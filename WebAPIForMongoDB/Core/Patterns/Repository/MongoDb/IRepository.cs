using MongoDB.Bson;
using System.Linq.Expressions;
using WebAPIForMongoDB.Core.Persistence.MongoDB;

namespace WebAPIForMongoDB.Core.Patterns.Repository.MongoDb
{
    public interface IRepository<T, in TKey> where T : class, IEntityMongoDb<TKey>, new() where TKey : IEquatable<TKey>
    {
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(TKey id);
        T GetById(ObjectId id);
        Task<T> AddAsync(T entity);
        Task<bool> AddRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(TKey id, T entity);
        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAsync(TKey id);
        Task<T> DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}
