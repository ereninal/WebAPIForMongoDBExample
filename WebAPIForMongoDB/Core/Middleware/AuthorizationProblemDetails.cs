using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPIForMongoDB.Core.Middleware
{
    public class AuthorizationProblemDetails : ProblemDetails
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
