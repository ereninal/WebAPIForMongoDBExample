using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPIForMongoDB.Core.Middleware
{
    public class BusinessProblemDetails : ProblemDetails
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
