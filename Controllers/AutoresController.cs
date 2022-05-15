using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebeVC.Models;

namespace PruebeVC.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            var listAutor = await context.Autores.Include(a => a.Libros).ToListAsync();
            return listAutor;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Autor autor)
        {
            var exist = await context.Autores.AnyAsync(a => a.Id == autor.Id);

            if(!exist)
            {
                return NotFound("The register inserted not exist");
            }

            context.Autores.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Autores.AnyAsync(a => a.Id == id);

            if(!exist)
            {
                return NotFound("The register inserted not exist");
            }

            context.Autores.Remove(new Autor(){Id = id});
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}