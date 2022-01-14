using Sharperio.Models;
using Sharperio.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Sharperio.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dbContext;
        private readonly IConfiguration _configuration;

        private Task<int> Save()
        {
            return _dbContext.SaveChangesAsync();
        }
        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }
        // public async Task<ServiceResponse<string>> Login(string username, string password)
        // {
        //     var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        //     if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        //     {
        //         return new ServiceResponse<string>
        //         {
        //             Message = "Failed to login",
        //             Success = false
        //         };
        //     }
        //     return new ServiceResponse<string>
        //     {
        //         Data = CreateToken(user),
        //     };

        // }

        // public async Task<ServiceResponse<int>> Register(AppUser user, string password)
        // {
        //     return new ServiceResponse<int>()
        //     {
        //     };
        // }

        // public async Task<bool> UserExists(string username)
        // {
        //     return await _dbContext.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        // }

        // private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordsSalt)
        // {
        //     using var hmack = new System.Security.Cryptography.HMACSHA512();
        //     passwordsSalt = hmack.Key;
        //     passwordHash = hmack.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        // }

        // private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordsSalt)
        // {
        //     using var hmack = new System.Security.Cryptography.HMACSHA512(passwordsSalt);
        //     return passwordHash.SequenceEqual(hmack.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        // }

        // private string CreateToken(AppUser user)
        // {
        //     var claims = new List<Claim>
        //     {
        //     };

        //     var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(claims),
        //         Expires = DateTime.Now.AddDays(100),
        //         SigningCredentials = creds
        //     };

        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var token = tokenHandler.CreateToken(tokenDescriptor);

        //     return tokenHandler.WriteToken(token);
        // }
    }
}