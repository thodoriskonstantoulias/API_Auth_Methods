namespace ApiAuth.ApiKey.Services
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string? apiKey);
    }
}
