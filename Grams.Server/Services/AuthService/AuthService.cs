
using System.Security.Cryptography;

namespace Grams.Server.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly DataContext _context;

    public AuthService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        var response = new ServiceResponse<int>();

        try
        {
            if (await Exists(user.Username))
            {
                response.Success = false;
                response.Message = "Username taked";

                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash);

            user.PasswordHash = passwordHash;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.Data = user.Id;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
    public Task<ServiceResponse<int>> Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> Exists(string username)
    {
        return await _context.Users.AnyAsync(
            u => u.Username == username
        );
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash)
    {
        var hmac = new HMACSHA512();
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}
