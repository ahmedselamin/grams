using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Grams.Server.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;

    public AuthService(DataContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
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
    public async Task<ServiceResponse<string>> Login(string username, string password)
    {
        var response = new ServiceResponse<string>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Not found";

                return response;
            }
            if (!VerifyPassword(password, user.PasswordHash))
            {
                response.Success = false;
                response.Message = "Wrong password";

                return response;
            }
            else
            {
                response.Data = CreateToken(user);
                response.Message = $"Welcome back, ${user.Username}";

                return response;
            }

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));


        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken
            (
              claims: claims,
              //expires: DateTime.Now.AddHours(1),
              expires: DateTime.Now.AddDays(1),
              signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private async Task<bool> Exists(string username)
    {
        return await _context.Users.AnyAsync(
            u => u.Username == username
        );
    }

    private bool VerifyPassword(string password, byte[] passwordHash)
    {
        var hmac = new HMACSHA512();

        var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        return computeHash.SequenceEqual(passwordHash);
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash)
    {
        var hmac = new HMACSHA512();
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}
