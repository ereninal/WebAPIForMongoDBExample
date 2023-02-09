﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using WebAPIForMongoDB.Core.Settings.MongoDB;
using WebAPIForMongoDB.Entities.MongoDB;

namespace WebAPIForMongoDB.Core.Patterns.Repository.MongoDb
{
    public abstract class MongoDbRepositoryBase<T> : IRepository<T, string> where T : MongoDbEntity, new()
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

        public virtual Task<T> GetByIdAsync(string id)
        {
            return Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await Collection.InsertOneAsync(entity, options);
            return entity;
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            //var data = (IEnumerable<WriteModel<T>>);
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            await Collection.InsertManyAsync(entities);
            return (await Collection.BulkWriteAsync((IEnumerable<WriteModel<T>>)entities, options)).IsAcknowledged;
        }

        public virtual async Task<T> UpdateAsync(string id, T entity)
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

        public virtual async Task<T> DeleteAsync(string id)
        {
            return await Collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public virtual async Task<T> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            return await Collection.FindOneAndDeleteAsync(filter);
        }
    }
}
