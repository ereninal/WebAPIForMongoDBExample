namespace WebAPIForMongoDB.Core.Middleware
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message)
        {
        }
    }
}
