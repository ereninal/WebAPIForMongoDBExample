namespace WebAPIForMongoDB.Core.Persistence.MongoDB
{
    public interface IEntityMongoDb
    {
    }
    public interface IEntityMongoDb<out TKey> : IEntityMongoDb where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }
        DateTime CreatedDate { get; set; }
    }
}
