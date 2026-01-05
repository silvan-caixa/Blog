using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Controllers;

[ApiController]
public class CategoriaController : ControllerBase
    {
    [HttpGet("categorias")]
    public async Task<IActionResult> GetAsync([FromServices] DbBlogContext context)
        {
        try
            {
            var categorias = await context.Categorias.ToListAsync();
            //return Ok(categorias);
            return Ok(new ResultViewModel<List<Categoria>>(categorias));

            }
        //catch (Exception ex)
        catch (Exception ex)

            {
            //return StatusCode(500, $"código => c01x01 - {ex.Message}");
            return StatusCode(500, new ResultViewModel<List<Categoria>>($"código => c01x01 - {ex.Message}"));
            }
        }

    [HttpGet("categorias/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromServices] DbBlogContext context, int id)
        {
        try
            {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            if (categoria == null)
                //return NotFound();
                return NotFound(new ResultViewModel<Categoria>($"Conteudo não encontrado"));
            //return Ok(categoria);
            return Ok(new ResultViewModel<Categoria>(categoria));
            }
        catch (Exception ex)
            {
            //return StatusCode(500, $"código c02x01 - {ex.Message}");
            return StatusCode(500, new ResultViewModel<List<Categoria>>($"código c02x02 - {ex.Message}"));
            }

        }

    //[HttpPost("categorias")]
    //public async Task<IActionResult> PostAsync([FromServices] DbBlogContext context, [FromBody] Models.Categoria model)
    //    {
    //    try
    //        {
    //        await context.Categorias.AddAsync(model);
    //        await context.SaveChangesAsync();
    //        return Created($"/categorias/{model.Id}", model);
    //        }
    //    catch (DbUpdateException ex)
    //        {
    //        return StatusCode(500, $"error: 03x01 Não foi possivel incluir a categoria {ex.Message}");
    //        }
    //    catch (Exception ex)
    //        {
    //        return StatusCode(500, $"error: 03x02 Falha interna no servidor {ex.Message}");
    //        }
    //    }

    [HttpPost("categorias")]
    public async Task<IActionResult> PostAsync(
        [FromServices] DbBlogContext context,
        [FromBody] EditorCategoriaViewModel model)
        {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values);

        try
            {
            var categoria = new Categoria
                {
                Nome = model.Nome,
                Slug = model.Slug.ToLower()
                };
            await context.Categorias.AddAsync(categoria);
            await context.SaveChangesAsync();
            return Created($"/categorias/{categoria.Id}", categoria);
            }
        catch (DbUpdateException ex)
            {
            return StatusCode(500, $"error: 03x01 Não foi possivel incluir a categoria {ex.Message}");
            }
        catch (Exception ex)
            {
            return StatusCode(500, $"error: 03x02 Falha interna no servidor {ex.Message}");
            }
        }
    //[HttpPut("categorias/{id}")]
    //public async Task<IActionResult> PutAsync([FromServices] DbBlogContext context, int id, [FromBody] Models.Categoria model)
    //    {
    //    try
    //        {
    //        var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
    //        if (categoria == null)
    //            return NotFound();

    //        categoria.Nome = model.Nome;
    //        categoria.Slug = model.Slug;

    //        context.Categorias.Update(categoria);
    //        await context.SaveChangesAsync();
    //        return Ok(categoria);
    //        }
    //    catch (Exception ex)
    //        {
    //        return StatusCode(500, $"error: 04x01 Falha interna => {ex.Message}");
    //        }

    //    }
    [HttpPut("categorias/{id}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] DbBlogContext context, int id,
        [FromBody] EditorCategoriaViewModel model)
        {
        try
            {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            if (categoria == null)
                return NotFound();

            categoria.Nome = model.Nome;
            categoria.Slug = model.Slug.ToLower();

            context.Categorias.Update(categoria);
            await context.SaveChangesAsync();
            return Ok(categoria);
            }
        catch (Exception ex)
            {
            return StatusCode(500, $"error: 04x01 Falha interna => {ex.Message}");
            }

        }
    [HttpDelete("categorias/{id}")]
    public async Task<IActionResult> DeleteAsync([FromServices] DbBlogContext context, int id)
        {
        try
            {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            if (categoria == null)
                return NotFound();

            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();
            return NoContent();
            }
        catch (Exception ex)
            {
            return StatusCode(500, $"error: 05x01 Falha interna => {ex.Message}");
            }

        }
    }

