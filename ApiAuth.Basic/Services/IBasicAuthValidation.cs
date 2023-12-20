namespace ApiAuth.Basic.Services
{
    public interface IBasicAuthValidation
    {
        bool IsValidBasicCreds(string? basicAuthKey);
    }
}
