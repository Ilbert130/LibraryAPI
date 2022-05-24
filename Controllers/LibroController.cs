using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebeVC.DTOs;
using PruebeVC.Models;

namespace PruebeVC.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibroController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroDTO>>> Get()
        {
            var listLibro = await context.Libros.ToListAsync();
            return mapper.Map<List<LibroDTO>>(listLibro);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libro = await context.Libros.FirstOrDefaultAsync(l => l.Id == id);
            return mapper.Map<LibroDTO>(libro);
        } 

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            var exist = await context.Libros.AnyAsync(l => l.Titulo == libroCreacionDTO.Titulo);

            if(exist)
            {
                return BadRequest("The register inserted exist");
            }

            var libro = mapper.Map<Libro>(libroCreacionDTO);

            context.Libros.Add(libro);
            await  context.SaveChangesAsync();
            return Ok();
        }
    }
}