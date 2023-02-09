namespace WebAPIForMongoDB.Entities.MongoDB
{
    public class Customer : MongoDbEntity
    {
        public string Fullname { get; set; }
        public int Age { get; set; }
       
    }
}
