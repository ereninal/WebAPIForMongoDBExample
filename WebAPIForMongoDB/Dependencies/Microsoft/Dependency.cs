using WebAPIForMongoDB.Core.Settings.MongoDB;
using WebAPIForMongoDB.DataAccess.Base;
using WebAPIForMongoDB.DataAccess.Repository;

namespace WebAPIForMongoDB.Dependencies.Microsoft
{

    public static class Dependency
    {
        public static IServiceCollection AddDepencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<ICustomerRepository, CustomerRepository>();

           
            
            //services.Configure<MongoDbSettings>(options =>
            //{
            //    options.Host = configuration
            //        .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.HostValue).Value;
            //    options.Port = configuration
            //        .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.PortValue).Value;
            //    options.ConnectTimeout = configuration
            //        .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectTimeoutValue).Value;
            //    options.ServerSelectionTimeout = configuration
            //        .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ServerSelectionTimeoutValue).Value;
            //    options.SocketTimeout = configuration
            //        .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.SocketTimeoutValue).Value;  
            //    options.DatabaseName = configuration
            //        .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseNameValue).Value;
               
            //});

            MongoDbSettings mongoConfig = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            services.AddSingleton(mongoConfig);
            return services;

        }
    }



}
