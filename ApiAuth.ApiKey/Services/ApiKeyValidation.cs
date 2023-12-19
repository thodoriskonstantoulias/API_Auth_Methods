namespace ApiAuth.ApiKey.Services
{
    public class ApiKeyValidation : IApiKeyValidation
    {
        private readonly IConfiguration configuration;

        public ApiKeyValidation(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool IsValidApiKey(string? apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return false;
            }

            var validApiKey = this.configuration.GetValue<string>(Models.Constants.ApiKey);

            return validApiKey == apiKey;
        }
    }
}
