using MongoDB.Driver.Core.Configuration;

namespace WebAPIForMongoDB.Core.Settings.MongoDB
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectTimeout { get; set; }
        public string ServerSelectionTimeout { get; set; }
        public string SocketTimeout { get; set; }


        #region Const Values

        public const string HostValue = nameof(Host);
        public const string PortValue = nameof(Port);
        public const string DatabaseNameValue = nameof(DatabaseName);
        public const string ConnectTimeoutValue = nameof(ConnectTimeout);
        public const string ServerSelectionTimeoutValue = nameof(ServerSelectionTimeout);
        public const string SocketTimeoutValue = nameof(SocketTimeout);

        #endregion

    }
}
