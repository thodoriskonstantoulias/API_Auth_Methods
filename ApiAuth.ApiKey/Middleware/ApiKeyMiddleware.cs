using ApiAuth.ApiKey.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.ApiKey.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IApiKeyValidation apiKeyValidation)
        {
            var userApiKey = context.Request.Headers[Models.Constants.ApiKeyHeaderName].ToString();

            if (string.IsNullOrWhiteSpace(userApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest; 
                return;
            }

            if (!apiKeyValidation.IsValidApiKey(userApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
