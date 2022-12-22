namespace Api.Features.Auth;

public sealed class LoginResponse
{
	public required string Jwt { get; set; }
}