using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Data.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AccountController(TokenService tokenService)
    {
        _tokenService =  tokenService;
    }
    [HttpGet("v1/login")]
    public IActionResult Login([FromServices] TokenService tokenService)
    {
        //var tokenService = new TokenService();
        var token = _tokenService.GenerateToken(new User());
        
        return Ok(token);
    }
}