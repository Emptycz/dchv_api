using System.Security.Claims;
using dchv_api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace dchv_api.Services;

public sealed class JwtManager
{
    private readonly ILogger<JwtManager> _logger;
    private readonly IConfiguration _configuration ;
    public JwtManager(ILogger<JwtManager> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public JwtSecurityToken GenerateToken(Login user)
    {
        var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
        if (jwt is null) throw new NullReferenceException("Could not load `JWT` section in appsetings");
        List<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            // new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Sid, user.ID.ToString()),
            new Claim(ClaimTypes.PrimarySid, user?.Persons?.FirstOrDefault()?.ID.ToString() ?? ""),
        };
        SymmetricSecurityKey key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(jwt.Key)
        );
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new JwtSecurityToken(
            jwt.Issuer,
            jwt.Audience,
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signIn
        );
        return token;
    }
}

internal sealed class Jwt
{
    public string Key { get; set; } = String.Empty;
    public string Issuer { get; set; } = String.Empty;
    public string Audience { get; set; } = String.Empty;
    public string Subject { get; set; } = String.Empty;
}

