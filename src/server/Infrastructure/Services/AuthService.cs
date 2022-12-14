using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Configurations.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public interface IAuthService
{
	Task<string> IssueJwtAsync(string username);
}

public sealed class AuthService : IAuthService
{
	private readonly JwtSettings _jwtSettings;

	public AuthService(IOptions<JwtSettings> jwtSettingsOptions)
	{
		_jwtSettings = jwtSettingsOptions.Value;
	}

	public Task<string> IssueJwtAsync(string username)
	{
		var issuer = _jwtSettings.Issuer;
		var audience = _jwtSettings.Audience;
		var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.NameIdentifier, username)
			}),
			Issuer = issuer,
			Audience = audience,
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
		};
		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);
		var jwtToken = tokenHandler.WriteToken(token);

		return Task.FromResult(jwtToken);
	}
}