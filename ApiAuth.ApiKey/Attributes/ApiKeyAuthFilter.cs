using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ApiAuth.ApiKey.Services;
using System.Text.Json;
using ApiAuth.ApiKey.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ApiAuth.ApiKey.Attributes
{
    public class ApiKeyAuthFilter : IAsyncAuthorizationFilter
    {
        private readonly IApiKeyValidation apiKeyValidation;

        public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
        {
            this.apiKeyValidation = apiKeyValidation;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Will use 3 methods for get the api key
            // 1) query string
            // 2) body
            // 3) headers
            string? userApiKey = null;

            // 1
            // userApiKey = context.HttpContext.Request.Query["apikey"].ToString();
           
            // 2
            //var body = context.HttpContext.Request.Body;
            //using (var reader = new StreamReader(body))
            //{
            //    var postData = await reader.ReadToEndAsync();
            //    if (!string.IsNullOrWhiteSpace(postData))
            //    {
            //       // userApiKey = JsonSerializer.Deserialize<ApiKeyModel>(postData)?.ApiKey;
            //        userApiKey = JsonConvert.DeserializeObject<ApiKeyModel>(postData)?.ApiKey;
            //    }
            //}

            //3
            userApiKey = context.HttpContext.Request.Headers[Models.Constants.ApiKeyHeaderName].ToString();

            if (string.IsNullOrWhiteSpace(userApiKey))
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (!apiKeyValidation.IsValidApiKey(userApiKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
