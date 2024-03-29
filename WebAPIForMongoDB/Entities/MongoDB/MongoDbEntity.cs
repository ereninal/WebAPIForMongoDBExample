﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WebAPIForMongoDB.Core.Persistence.MongoDB;

namespace WebAPIForMongoDB.Entities.MongoDB
{
    public abstract class MongoDbEntity : IEntityMongoDb<ObjectId>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        public ObjectId Id { get; } = ObjectId.GenerateNewId();

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
