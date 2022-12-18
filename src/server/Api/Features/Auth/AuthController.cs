using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Auth;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost]
	[AllowAnonymous]
	public async Task<IActionResult> Login([FromBody] LoginRequest request)
	{
		var jwtToken = await _authService.LoginAsync(request.Username);

		var response = new LoginResponse
		{
			Jwt = jwtToken
		};
		return Ok(response);
	}
}