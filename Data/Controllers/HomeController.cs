using Microsoft.AspNetCore.Mvc;

namespace Blog.Data.Controllers
    {

    [ApiController]
    public class HomeController : ControllerBase
        {
        [HttpGet("test-conexao")]
        public IActionResult Get(DbBlogContext context)
            {
            DbBlogContext.TextConexao();

            return Ok("Conexão testada. Verifique o console para detalhes.");
            }
        }
    }
