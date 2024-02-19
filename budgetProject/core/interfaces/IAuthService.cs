namespace core.interfaces;

public interface IAuthService
{
    Task<string> RegisterUser(string email, string password);
    Task<string> SignInUser(token token);
}