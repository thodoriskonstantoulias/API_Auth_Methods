using System.Reflection;

namespace ApiAuth.Basic.Services
{
    public class BasicAuthValidation : IBasicAuthValidation
    {
        private readonly IConfiguration configuration;

        public BasicAuthValidation(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool IsValidBasicCreds(string? basicAuthKey)
        {
            if (string.IsNullOrWhiteSpace(basicAuthKey))
            {
                return false;
            }

            var user = this.configuration.GetValue<string>("Basic:Username");
            var pass = this.configuration.GetValue<string>("Basic:Password");

            byte[] authHeaderValue = Convert.FromBase64String(basicAuthKey.Split(" ")[1]);
            string decodedString = System.Text.Encoding.UTF8.GetString(authHeaderValue);
            var arr = decodedString.Split(':');

            return arr[0].Equals(user) && arr[1].Equals(pass); 
        }
    }
}
