namespace WebAPIForMongoDB.Core.Middleware
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}
