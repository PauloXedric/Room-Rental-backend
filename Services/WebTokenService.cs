using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RRMS.Models.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RRMS.Services
{
    public interface IWebTokenService
    {
        Task<string> GenerateUserTokenAsync(ApplicationUser identityUser);
    }


    public class WebTokenService : IWebTokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<WebTokenService> _logger;
        public WebTokenService(IConfiguration config, UserManager<ApplicationUser> userManager,
                               ILogger<WebTokenService> logger) 
        {
            _config = config;
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<string> GenerateUserTokenAsync(ApplicationUser identityUser)
        {
            try
            {
                var roles = await _userManager.GetRolesAsync(identityUser);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                    new Claim(ClaimTypes.Email, identityUser.Email ?? string.Empty),                    
                    new Claim("Status", identityUser.Status.ToString()),
                };

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                return BuildToken(claims, TimeSpan.FromHours(24));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating log in web token for user with email: {Email}", identityUser.Email);
                throw;
            }
        }

        private string BuildToken(IEnumerable<Claim> claims, TimeSpan validFor)
        {
            var jwtKey = _config["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                _logger.LogCritical("JWT key is missing in configuration. Cannot continue.");
                throw new InvalidOperationException("Missing JWT signing key.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.Add(validFor),
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                signingCredentials: signingCred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
