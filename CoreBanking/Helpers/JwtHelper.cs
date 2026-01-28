using CoreBanking.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreBanking.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;
        //private readonly UserManager<Customer> _userManager;
        private readonly IConfigurationSection _jwtSettings;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            //_userManager = userManager;
            _jwtSettings = _configuration.GetSection("JWTSettings");

        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public List<Claim> GetClaims(Customer user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            return claims;
        }

        public JwtSecurityToken GenerateTokenOption(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                   issuer: _jwtSettings["validIssuer"],
                   audience: _jwtSettings["validAudience"],
                   claims: claims,
                   expires: DateTime.Now.AddHours(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
                   signingCredentials: signingCredentials
               );
            return tokenOptions;
        }

        public string GenerateToken(Customer user)
        {
            var signinCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokeOptions = GenerateTokenOption(signinCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return token;
        }
    }
}
