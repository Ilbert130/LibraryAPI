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
            var libro = await context.Libros.Include(lb => lb.AutoresLibros)
            .ThenInclude(al => al.Autor).FirstOrDefaultAsync(l => l.Id == id);

            libro.AutoresLibros = libro.AutoresLibros.OrderByDescending(x => x.Orden).ToList();

            return mapper.Map<LibroDTO>(libro);
        } 

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            if(libroCreacionDTO.AutoresIds == null)
            {
                return BadRequest("Doesn't can creat a libro withaout autores");
            }

            var autores = await context.Autores.Where(aut => libroCreacionDTO.AutoresIds.Contains(aut.Id)).Select(x => x.Id).ToListAsync();

            if(libroCreacionDTO.AutoresIds.Count != autores.Count)
            {
                return BadRequest("The register inserted not exist");
            }

            var libro = mapper.Map<Libro>(libroCreacionDTO);

            if(libro.AutoresLibros != null)
            {
                for(int i = 0; i< libro.AutoresLibros.Count; i++)
                {
                    libro.AutoresLibros[i].Orden = i;
                }
            }

            context.Libros.Add(libro);
            await  context.SaveChangesAsync();
            return Ok();
        }
    }
}