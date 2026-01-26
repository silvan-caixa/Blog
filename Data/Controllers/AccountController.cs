using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Extensions;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;


namespace Blog.Data.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly TokenService _tokenService;
    
    public AccountController(TokenService tokenService)
    {
        _tokenService =  tokenService;
    }

    [HttpPost("v1/login")]
    public async Task<IActionResult> AsyncLogin(
        [FromBody] LoginViewModel model, 
        [FromServices] DbBlogContext context)
    {
     if(!ModelState.IsValid)  BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));   
     
     var user = await context.Users
         .AsNoTracking()
         .Include(u => u.Roles)
         .FirstOrDefaultAsync(u => u.Email == model.Email);
     if(user == null) StatusCode(400, new ResultViewModel<string>("Usuario ou email invalido"));
     if(!PasswordHasher.Verify(user.PasswordHash, model.Password)) StatusCode(400, new ResultViewModel<string>("Usuario ou email invalido"));
     try
     {
         var token = _tokenService.GenerateToken(user);
         return Ok(new ResultViewModel<string>(token, null));
     }
     catch
     {
         return StatusCode(500, new ResultViewModel<string>("Erro ao tentar acessar"));
     }
    
    }

    [HttpPost("v1/account/")]
    public async Task<IActionResult> AsyncPost([FromBody] RegisterViewModel model, [FromServices] DbBlogContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Email.Replace("@", "_").Replace(".", "-")
        };
        var password = PasswordGenerator.Generate(25);
        user.PasswordHash = PasswordHasher.Hash(password);

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.Email, password
            }));
        }
        catch(DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("05X99 ESTE EMAIL JA EXISTE"));
        }
    }
    
    [HttpGet("v1/login")]
    public IActionResult Login(
        [FromServices] TokenService tokenService)
    {
        //var tokenService = new TokenService();
        var user = context.Users.AsNoTracking()
            .Include(u => u.Roles)
            .FirstOrDefault(u => u.Email == User.Identity.Name);
        var token = _tokenService.GenerateToken(user);
        
        return Ok(token);
    }
    // [Authorize(Roles = "user")]
    // [HttpGet("v1/user")]
    // public IActionResult GetUser() => Ok(User.Identity.Name);
    //
    // [HttpGet("v1/author")]
    // public IActionResult GetAuthor() => Ok(User.Identity.Name);
    // [Authorize(Roles = "admin")]
    // [HttpGet("v1/admin")]
    // public IActionResult GetAdmin() => Ok(User.Identity.Name);
}