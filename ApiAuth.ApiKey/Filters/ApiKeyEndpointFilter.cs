
using ApiAuth.ApiKey.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.ApiKey.Filters
{
    public class ApiKeyEndpointFilter : IEndpointFilter
    {
        private readonly IApiKeyValidation _apiKeyValidation;
        public ApiKeyEndpointFilter(IApiKeyValidation apiKeyValidation)
        {
            _apiKeyValidation = apiKeyValidation;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var userApiKey = context.HttpContext.Request.Headers[Models.Constants.ApiKeyHeaderName].ToString();

            if (string.IsNullOrWhiteSpace(userApiKey))
            {
                return Results.BadRequest();
            }

            if (!_apiKeyValidation.IsValidApiKey(userApiKey))
            {
                return Results.Unauthorized();
            }

            return await next(context);
        }
    }
}
