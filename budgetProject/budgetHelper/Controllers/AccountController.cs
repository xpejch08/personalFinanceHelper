using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Dynamic;
using core;
using core.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace budgetHelper.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] registerInfoDto registerInfoDto)
    {
        var token = await _authService.RegisterUser(registerInfoDto.Email, registerInfoDto.Password);
        if (!string.IsNullOrEmpty(token))
        {
            return Ok(new { Token = token });
        }
        return BadRequest("Registration failed.");
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] token token)
    {
        var result = await _authService.SignInUser(token);
        if (!string.IsNullOrEmpty(result))
        {
            return Ok(new { Token = result });
        }
        return BadRequest("Sign in failed.");
    }

    // ... other actions for reset password, sign out, etc.
}
