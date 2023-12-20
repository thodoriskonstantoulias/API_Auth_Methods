using ApiAuth.ApiKey.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.ApiKey.Attributes
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute(): base(typeof(ApiKeyAuthFilter))
        {
        }
    }
}
