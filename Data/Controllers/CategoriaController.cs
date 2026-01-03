using Microsoft.AspNetCore.Mvc;

namespace Blog.Data.Controllers;

[ApiController]
public class CategoriaController : ControllerBase
    {
    [HttpGet("categorias")]
    public IActionResult Get([FromServices] DbBlogContext context)
        {
        var categorias = context.Categorias.ToList();
        return Ok(categorias);
        }

    [HttpGet("categorias/{id}")]
    public IActionResult GetById([FromServices] DbBlogContext context, int id)
        {
        var categoria = context.Categorias.FirstOrDefault(x => x.Id == id);
        if (categoria == null)
            return NotFound();
        return Ok(categoria);
        }

    [HttpPost("categorias")]
    public IActionResult Post([FromServices] DbBlogContext context, [FromBody] Models.Categoria model)
        {
        context.Categorias.Add(model);
        context.SaveChanges();
        return Created($"/categorias/{model.Id}", model);
        }

    [HttpPut("categorias/{id}")]
    public IActionResult Put([FromServices] DbBlogContext context, int id, [FromBody] Models.Categoria model)
        {
        var categoria = context.Categorias.FirstOrDefault(x => x.Id == id);
        if (categoria == null)
            return NotFound();

        categoria.Nome = model.Nome;
        categoria.Slug = model.Slug;

        context.Categorias.Update(categoria);
        context.SaveChanges();
        return Ok(categoria);
        }
    [HttpDelete("categorias/{id}")]
    public IActionResult Delete([FromServices] DbBlogContext context, int id)
        {
        var categoria = context.Categorias.FirstOrDefault(x => x.Id == id);
        if (categoria == null)
            return NotFound();

        context.Categorias.Remove(categoria);
        context.SaveChanges();
        return NoContent();
        }
    }

