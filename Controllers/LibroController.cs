using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebeVC.Models;

namespace PruebeVC.Controllers
{
    [ApiController]
    [Route("api/libro")]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibroController(ApplicationDbContext context)
        {
            this.context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Libro>>> Get()
        {
            var listLibro = await context.Libros.Include(l => l.Autor).ToListAsync();
            return listLibro;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var listLibro = await context.Libros.Include(l => l.Autor).FirstOrDefaultAsync(l => l.AutorId == id);
            return listLibro;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var exist = await context.Libros.AnyAsync(l => l.Id == libro.Id);

            if(exist)
            {
                return BadRequest("The register inserted exist");
            }

            context.Libros.Add(libro);
            await  context.SaveChangesAsync();
            return Ok();
        }
    }
}