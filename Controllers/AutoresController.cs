using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebeVC.DTOs;
using PruebeVC.Models;

namespace PruebeVC.Controllers
{
    [ApiController]
    [Route("api/autores")] // api/autores => Route
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{name}")] // api/autores/id => Route
        public async Task<ActionResult<AutorDTO>> Get(string name)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(a => a.Nombre.Contains(name));
            return mapper.Map<AutorDTO>(autor);
        }

        [HttpGet("list/{name}")]
        public async Task<ActionResult<List<AutorDTO>>> GetList(string name)
        {
            var listAutor = await context.Autores.Where(a => a.Nombre.Contains(name)).ToListAsync();
            return mapper.Map<List<AutorDTO>>(listAutor);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDTO)
        {
            var exiteAutorConElMismoNombre = await context.Autores.AnyAsync(a => a.Nombre == autorCreacionDTO.Nombre);

            if (exiteAutorConElMismoNombre)
            {
                return BadRequest("Ya exite el registro a crear");
            }

            var autor= mapper.Map<Autor>(autorCreacionDTO);

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Autor autor)
        {
            var exist = await context.Autores.AnyAsync(a => a.Id == autor.Id);

            if (!exist)
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

            if (!exist)
            {
                return NotFound("The register inserted not exist");
            }

            context.Autores.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}