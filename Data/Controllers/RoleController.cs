using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Controllers
    {
    public class RoleController : ControllerBase
        {
        [HttpGet("roles")]
        public async Task<IActionResult> GetAsync([FromServices] DbBlogContext context)
            {
            try
                {
                var roles = await context.Roles.ToListAsync();
                return Ok(new ResultViewModel<List<Role>>(roles));
                }
            catch (Exception ex)
                {
                return StatusCode(500, new ResultViewModel<List<Role>>($"código => r01x01 - {ex.Message}"));
                }
            }
        }
    }
