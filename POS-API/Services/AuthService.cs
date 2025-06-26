using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using POS_API.BusinessObjects;
using POS_API.Interfaces;
using POS_API.Model;
using POS_API.Models;

namespace POS_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly POSEntities db;
        private readonly SQLService _sqlService;
        private readonly IConfiguration _config;
        

        public AuthService(DbContextOptions<POSEntities> options, SQLService sqlService, IConfiguration config)
        {
            db = new POSEntities(options);
            _sqlService = sqlService;
            _config = config;
        }

        public async Task<string> AuthenticateAsync(LoginRequest request)
        {
            var user = await db.tblUsers
                .FirstOrDefaultAsync(u => u.varEmail == request.varEmail && u.varAuthProvider == "Local");

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.varPassword, user.varPassword))
                throw new UnauthorizedAccessException("Invalid credentials");
            return GenerateJwt(user);
        }

        public async Task<string> GoogleAuthenticateAsync(string idToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            var user = await db.tblUsers
                .FirstOrDefaultAsync(u => u.varExternalId == payload.Subject && u.varAuthProvider == "Google");

            if (user == null)
            {
                user = new tblUser
                {
                    varEmail = payload.Email,
                    varName = payload.Name,
                    varAuthProvider = "Google",
                    varExternalId = payload.Subject,
                    intCompanyId = 1, // <- tenant logic later
                    dtCreationDate = DateTime.UtcNow
                };
                db.tblUsers.Add(user);
                await db.SaveChangesAsync();
            }

            return GenerateJwt(user);
        }
        private string GenerateJwt(tblUser user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.intUserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.varEmail),
            new Claim("tenant", user.intCompanyId?.ToString() ?? ""),
            new Claim("provider", user.varAuthProvider),
            new Claim("role", user.isAdmin == true ? "Admin" : "User")
        };
            var jwtKey = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("Jwt:Key is not configured in the application settings.");
            }
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
