namespace Grams.Server.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<ServiceResponse<int>> Login(string username, string password);
}
