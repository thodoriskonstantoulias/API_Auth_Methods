using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using ApiAuth.Basic.Services;

namespace ApiAuth.Basic.Handlers
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly IBasicAuthValidation _basicAuthValidation;
        public BasicAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IBasicAuthValidation basicAuthValidation) : base(options, logger, encoder, clock)
        {
            _basicAuthValidation = basicAuthValidation;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (authorizationHeader != null && authorizationHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                if (!this._basicAuthValidation.IsValidBasicCreds(authorizationHeader))
                {
                    Response.StatusCode = 401;
                    Response.Headers.Append("WWW-Authenticate", "Basic realm=\"ted.com\"");
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                }

                var claims = new[] { 
                    new Claim(ClaimTypes.Name, authorizationHeader) 
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var claimsPrincipal = new ClaimsPrincipal(identity);
                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
            }

            Response.StatusCode = 401;
            Response.Headers.Append("WWW-Authenticate", "Basic realm=\"ted.com\"");
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
}
