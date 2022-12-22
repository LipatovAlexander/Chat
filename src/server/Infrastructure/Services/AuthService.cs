using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Infrastructure.Configurations.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public interface IAuthService
{
	Task<string> LoginAsync(string username);
}

public sealed class AuthService : IAuthService
{
	private readonly JwtSettings _jwtSettings;
	private readonly ApplicationDbContext _dbContext;

	public AuthService(IOptions<JwtSettings> jwtSettingsOptions, ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
		_jwtSettings = jwtSettingsOptions.Value;
	}

	public async Task<string> LoginAsync(string username)
	{
		var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
		if (user is null)
		{
			user = new User
			{
				Username = username
			};
			_dbContext.Users.Add(user);
			await _dbContext.SaveChangesAsync();
		}
	
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

		return jwtToken;
	}
}