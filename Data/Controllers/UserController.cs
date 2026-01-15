using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Controllers
    {
    public class UserController : ControllerBase
        {
        [HttpGet("User")]
        public async Task<IActionResult> GetAsync([FromServices] DbBlogContext context)
            {
            try
                {
                var users = await context.Users.ToListAsync();
                return Ok(new ResultViewModel<List<User>>(users));
                }
            catch (Exception ex)
                {
                return StatusCode(500, new ResultViewModel<List<User>>($"código => u01x01 - {ex.Message}"));
                }
            }
        }
    }
