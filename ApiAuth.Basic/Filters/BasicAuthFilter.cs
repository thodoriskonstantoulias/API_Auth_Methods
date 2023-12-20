using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ApiAuth.Basic.Services;

namespace ApiAuth.Basic.Filters
{
    public class BasicAuthFilter : IAsyncAuthorizationFilter
    {
        private readonly IBasicAuthValidation basicAuthValidation;

        public BasicAuthFilter(IBasicAuthValidation basicAuthValidation)
        {
            this.basicAuthValidation = basicAuthValidation;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userBasicAuthKey = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(userBasicAuthKey))
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (!basicAuthValidation.IsValidBasicCreds(userBasicAuthKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
