using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using WebAPIForMongoDB.Core.Settings.MongoDB;
using WebAPIForMongoDB.Entities.MongoDB;

namespace WebAPIForMongoDB.Core.Patterns.Repository.MongoDb
{
    public abstract class MongoDbRepositoryBase<T> : IRepository<T, ObjectId> where T : MongoDbEntity, new()
    {
        protected readonly IMongoCollection<T> Collection;
        private readonly MongoDbSettings settings;

        protected MongoDbRepositoryBase(IOptions<MongoDbSettings> options)
        {
            this.settings = options.Value;

            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(this.settings.Host, int.Parse(this.settings.Port));
            settings.ConnectTimeout = TimeSpan.FromMilliseconds(double.Parse(this.settings.ConnectTimeout));
            settings.ServerSelectionTimeout = TimeSpan.FromMilliseconds(double.Parse(this.settings.ServerSelectionTimeout));
            settings.SocketTimeout = TimeSpan.FromMilliseconds(double.Parse(this.settings.SocketTimeout));

            //var client = new MongoClient(this.settings.ConnectionString);
            var client = new MongoClient(settings);
            var db = client.GetDatabase(this.settings.DatabaseName);
            this.Collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
                ? Collection.AsQueryable()
                : Collection.AsQueryable().Where(predicate);
        }

        public virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public virtual Task<T> GetByIdAsync(ObjectId id)
        {
            return Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        } 
        
        public virtual T GetById(ObjectId id)
        {
            var data = Collection.AsQueryable().Where(m => m.Id == id).FirstOrDefault();
            return data;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await Collection.InsertOneAsync(entity, options);
            return entity;
        }

        public virtual async Task<bool> AddRangeAsync(IList<T> entities)
        {
            //var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            var options = new InsertManyOptions { IsOrdered = false, BypassDocumentValidation = false };
            await Collection.InsertManyAsync(entities, options);
            return true;//(await Collection.BulkWriteAsync((IList<WriteModel<T>>)entities, options)).IsAcknowledged;
        }

        public virtual async Task<T> UpdateAsync(ObjectId id, T entity)
        {
            return await Collection.FindOneAndReplaceAsync(x => x.Id == id, entity);
        }

        public virtual async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            return await Collection.FindOneAndReplaceAsync(predicate, entity);
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            return await Collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
        }

        public virtual async Task<T> DeleteAsync(ObjectId id)
        {
            return await Collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public virtual async Task<T> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            return await Collection.FindOneAndDeleteAsync(filter);
        }
    }
}
